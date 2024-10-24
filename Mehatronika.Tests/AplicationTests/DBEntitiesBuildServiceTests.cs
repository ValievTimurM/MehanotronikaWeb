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
using NSubstitute;

[TestFixture]
internal class DBEntitiesBuildServiceTests
{
  private IEntityService<Car> _carServiceMock;
  private IEntityService<Driver> _driverServiceMock;
  private ILogger<DBEntitiesBuildService> _loggerMock;

  [SetUp]
  public void SetUp()
  {
    _carServiceMock = Substitute.For<IEntityService<Car>>();
    _driverServiceMock = Substitute.For<IEntityService<Driver>>();
    _loggerMock = Substitute.For<ILogger<DBEntitiesBuildService>>();
  }

  [Test]
  public void BuildViewModels_ServicesReturnsEntities_BuildTwoModels()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.DbModels = new Stack<CarDriverViewModel>();
    object state = null;
    DBEntitiesBuildService _service = new DBEntitiesBuildService(_entityList, _loggerMock, _carServiceMock, _driverServiceMock);
    _carServiceMock.GetLast().Returns(Car.New("volvo"));
    _driverServiceMock.GetLast().Returns(Driver.New("Иваныч"));

    _service.BuildViewModels(state);
    int result = _entityList.DbModels.Count();

    Assert.That(result, Is.EqualTo(2));
  }



  [Test]
  public void BuildViewModels_CarAlreadyExistInDbList_NotAddedCarToList()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.DbModels = new Stack<CarDriverViewModel>();
    var car = Car.New("volvo");
    _entityList.DbModels.Push(CarDriverViewModel.New(car));
    object state = null;
    DBEntitiesBuildService _service = new DBEntitiesBuildService(_entityList, _loggerMock, _carServiceMock, _driverServiceMock);
    _carServiceMock.GetLast().Returns(car);
    _driverServiceMock.GetLast().Returns(Driver.New("Иваныч"));

    _service.BuildViewModels(state);
    int result = _entityList.DbModels.Count();

    Assert.That(result, Is.EqualTo(2));
  }

  [Test]
  public void BuildViewModels_DriverAlreadyExistInDbList_NotAddedDriverToList()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.DbModels = new Stack<CarDriverViewModel>();
    var driver = Driver.New("Иван");
    _entityList.DbModels.Push(CarDriverViewModel.New(driver));
    object state = null;
    DBEntitiesBuildService _service = new DBEntitiesBuildService(_entityList, _loggerMock, _carServiceMock, _driverServiceMock);
    _carServiceMock.GetLast().Returns(Car.New("volvo"));
    _driverServiceMock.GetLast().Returns(driver);

    _service.BuildViewModels(state);
    int result = _entityList.DbModels.Count();

    Assert.That(result, Is.EqualTo(2));
  }

  [Test]
  public void BuildViewModels_GetDriveThrowException_LoggerHasReceivedInCatchSection()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.DbModels = new Stack<CarDriverViewModel>();
    object state = null;
    var ex = new Exception("Test exception");
    _carServiceMock.GetLast().Returns(Car.New("volvo"));
    _driverServiceMock.GetLast().Returns(Task.FromException<Driver>(ex));
    DBEntitiesBuildService _service = new DBEntitiesBuildService(_entityList, _loggerMock, _carServiceMock, _driverServiceMock);

    _service.BuildViewModels(state);
    int result = _entityList.DbModels.Count();

    _loggerMock.Received(1).LogError(ex, "Ошибка при построении списка моделей из БД.");
    Assert.That(result, Is.EqualTo(0));
  }

  [Test]
  public void BuildViewModels_GetCarThrowException_LoggerHasReceivedInCatchSection()
  {
    var _timerMock = new ThreadTimer();
    var _entityList = EntityList.getInstance();
    _entityList.DbModels = new Stack<CarDriverViewModel>();
    object state = null;
    var ex = new Exception("Test exception");
    _carServiceMock.GetLast().Returns(Task.FromException<Car>(ex));
    _driverServiceMock.GetLast().Returns(Driver.New("volvo"));
    DBEntitiesBuildService _service = new DBEntitiesBuildService(_entityList, _loggerMock, _carServiceMock, _driverServiceMock);

    _service.BuildViewModels(state);
    int result = _entityList.DbModels.Count();

    _loggerMock.Received(1).LogError(ex, "Ошибка при построении списка моделей из БД.");
    Assert.That(result, Is.EqualTo(0));
  }

}

