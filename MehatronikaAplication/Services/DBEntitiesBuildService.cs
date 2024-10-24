using MehatronikaAplication.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MehatronikaAplication.Interfaces.Services;
using MehatronikaCore.Models;
using MehatronikaAplication.Model.ViewModels;

namespace MehatronikaAplication.Services
{
  public class DBEntitiesBuildService : IHostedService
  {
    private readonly EntityList _entityList;
    private IEntityService<Car> _carService;
    private IEntityService<Driver> _driveService;
    private Timer _timer;
    private ILogger _logger;

    public DBEntitiesBuildService(EntityList entityList,
                                ILogger<DBEntitiesBuildService> logger,
                                IEntityService<Car> carService,
                                IEntityService<Driver> driveService)
    {
      _entityList = entityList;
      _carService = carService;
      _driveService = driveService;
      _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _timer = new Timer(BuildViewModels, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

      return Task.CompletedTask;
    }

    public async void BuildViewModels(object state)
    {
      try
      {
        var driver = await _driveService.GetLast();
        var car = await _carService.GetLast();

        if (driver is not null && _entityList.DbModels.Any(p => p.Id == driver.Id) == false)
        {
          _entityList.DbModels.Push(CarDriverViewModel.New(driver));
        }
        if (car is not null && _entityList.DbModels.Any(p => p.Id == car.Id) == false)
        {
          _entityList.DbModels.Push(CarDriverViewModel.New(car));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Ошибка при построении списка моделей из БД.");
      }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _timer?.Change(Timeout.Infinite, 0);
      return Task.CompletedTask;
    }
  }
}
