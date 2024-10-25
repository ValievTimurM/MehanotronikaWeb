using MehatronikaAplication.Helpers;
using MehatronikaAplication.Interfaces.Services;
using MehatronikaAplication.Model;
using MehatronikaAplication.Model.ViewModels;
using MehatronikaAplication;
using MehatronikaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using MehatronikaAplication.ThTimer;
using Microsoft.Extensions.Logging;

namespace MehatronikaAplication.Services
{
  public class ThreadingService : IThreadingService
  {
    private readonly IEntityService<Car> _carService;
    private readonly IEntityService<Driver> _driveService;
    private readonly ThreadTimer _timer;
    private readonly EntityList _entityList;
    private readonly ILogger<ThreadingService> _logger;

    private Thread _carThread;
    private Thread _driverThread;
    private IList<string> initCars = new List<string>();
    private IList<string> initDrivers = new List<string>();
    private ManualResetEventSlim _thCarControl = new ManualResetEventSlim(true);
    private ManualResetEventSlim _thDriverControl = new ManualResetEventSlim(true);
    private bool _hasStarted;

    public ThreadingService(IEntityService<Car> carService,
                            IEntityService<Driver> driveService,
                            ThreadTimer timer,
                            EntityList entityList,
                            ILogger<ThreadingService> logger)
    {
      _carService = carService;
      _driveService = driveService;
      _timer = timer;
      _entityList = entityList;
      _carThread = new Thread(new ThreadStart(CarHandle));
      _driverThread = new Thread(new ThreadStart(DriverHandle));
      _logger = logger;
    }

    public void StopCarThread()
    {
      if (_carThread.IsAlive == false) return;
      _thCarControl.Reset();

      _logger.LogInformation("Остановка потока автомобилей");
      
    }

    public void ResumeCarThread()
    {
      if (_carThread.IsAlive == false) return;
      _thCarControl.Set();

      _logger.LogInformation("Продолжение потока автомобилей");
    }

    public void StopDriveThread()
    {
      if (_driverThread.IsAlive == false) return;
      _thDriverControl.Reset();

      _logger.LogInformation("Остановка потока водителей");
    }

    public void ResumeDriveThread()
    {
      if (_driverThread.IsAlive == false) return;
      _thDriverControl.Set();

      _logger.LogInformation("Продолжение потока водителей");
    }

    public void Start()
    {
      if (_hasStarted) return;
      _hasStarted = true;

      initCars = EntityHelper.GetCarModelNames();
      initDrivers = EntityHelper.GetDriversFio();

      _logger.LogInformation($"Запуск потоков");
      _timer.StartTimer(_carThread, _driverThread);

    }

    private void CarHandle()
    {
      _logger.LogInformation("Поток машин начал работу " + DateTime.Now);
      foreach (string el in initCars)
      {
        Thread.Sleep(2000);
        _thCarControl.Wait();
        Car car = Car.New(el);
        _entityList.Models.Push(CarDriverViewModel.New(car));

        SaveCar(car);
      }
    }

    private void DriverHandle()
    {
      _logger.LogInformation("Поток водителей начал работу " + DateTime.Now);
      int counter = 0;
      foreach (string el in initDrivers)
      {
        counter++;
        Thread.Sleep(3000);
        _thDriverControl.Wait();
        Driver driver = Driver.New(el);
        _entityList.Models.Push(CarDriverViewModel.New(driver));

        if (counter == 2)
        {
          _logger.LogInformation($"Совпадение временных меток потоков");
        }

        SaveDriver(driver);
      }
    }

    private void SaveCar(Car car)
    {
      Task.Run(async () =>
      {
        try
        {
          await _carService.Add(car);
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, $"Ошибка сохранения машины");
        }
      });
    }

    private void SaveDriver(Driver driver)
    {
      Task.Run(async () =>
      {
        try
        {
          await _driveService.Add(driver);
        }
        catch (Exception ex) 
        {
          _logger.LogError(ex,$"Ошибка сохранения водителя");
        }
      });
    }

  }
}
