using NUnit.Framework;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using MehatronikaAplication.Interfaces.Services;
using MehatronikaAplication.Services;
using MehatronikaAplication.ThTimer;
using MehatronikaAplication.Model;
using MehatronikaCore.Models;
using Microsoft.Extensions.Logging;
using MehatronikaAplication.Helpers;
using System.Diagnostics;
using MehatronikaAplication.Model.ViewModels;

[TestFixture]
public class ThreadingServiceTests
{

  private IEntityService<Car> _carServiceMock;
  private IEntityService<Driver> _driverServiceMock;
  private ILogger<ThreadingService> _loggerMock;

  [SetUp]
  public void SetUp()
  {
    _carServiceMock = Substitute.For<IEntityService<Car>>();
    _driverServiceMock = Substitute.For<IEntityService<Driver>>();
    _loggerMock = Substitute.For<ILogger<ThreadingService>>();
    _carServiceMock.Add(Arg.Any<Car>()).Returns(Task.CompletedTask);
    _driverServiceMock.Add(Arg.Any<Driver>()).Returns(Task.CompletedTask);
  }

  [Test]
  public void ThreadStart_WaitFiveSeconds_ThreeModelsExist()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.Models = new Stack<CarDriverViewModel>();
    ThreadingService _service = new ThreadingService(_carServiceMock, _driverServiceMock, _timerMock, _entityList, _loggerMock);

    _service.Start();

    Thread.Sleep(5000);
    _service.StopDriveThread();
    _service.StopCarThread();

    int result = _entityList.Models.Count();
    Assert.That(result, Is.EqualTo(3));
  }

  [Test]
  public void ThreadStart_WaitOneSeconds_ModelsNotExist()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.Models = new Stack<CarDriverViewModel>();
    ThreadingService _service = new ThreadingService(_carServiceMock, _driverServiceMock, _timerMock, _entityList, _loggerMock);

    _service.Start();
    _service.StopDriveThread();
    _service.StopCarThread();

    int result = _entityList.Models.Count();
    Assert.That(result, Is.EqualTo(0));
  }

  [Test]
  public void ThreadStart_StopCarThread_OneDriverExist()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.Models = new Stack<CarDriverViewModel>();
    ThreadingService _service = new ThreadingService(_carServiceMock, _driverServiceMock, _timerMock, _entityList, _loggerMock);

    _service.Start();
    _service.StopCarThread();

    Thread.Sleep(4000);
    _service.StopDriveThread();

    Assert.That(_entityList.Models.Count(), Is.EqualTo(1));
  }

  [Test]
  public void ThreadStart_StopCarThread_OneDriverExist1()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.Models = new Stack<CarDriverViewModel>();
    ThreadingService _service = new ThreadingService(_carServiceMock, _driverServiceMock, _timerMock, _entityList, _loggerMock);

    _service.Start();

    Thread.Sleep(5000);
    _service.StopDriveThread();
    _service.StopCarThread();

    Assert.That(_entityList.Models.Count(), Is.EqualTo(3));
    _carServiceMock.Received(2).Add(Arg.Any<Car>());
    _driverServiceMock.Received(1).Add(Arg.Any<Driver>());
  }
}