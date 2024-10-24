using MehatronikaAplication.Interfaces.Services;
using MehatronikaAplication.Model;
using MehatronikaAplication.Model.ViewModels;
using MehatronikaAplication.Services;
using MehatronikaCore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Tree;
using Radzen.Blazor;
using System.Threading;
using System.Timers;
using Timer = System.Threading.Timer;

namespace MehanotronikaWeb.Pages
{
  public partial class Index
  {
    [Inject]
    public IThreadingService _thService { get; set; } = default!;
    [Inject]
    public IEntityService<Car> _carService { get; set; } = default!;
    [Inject]
    public EntityList EntityList { get; set; }
    [Inject]
    public ILogger<Index> _logger { get; set; }

    private RadzenDataGrid<CarDriverViewModel> _modelsGrid;
    private Timer _timer;
    private bool _carThStopped;
    private bool _driverThStopped;

    protected override void OnInitialized()
    {
      _thService.Start();

      Task.Run(async () =>
      {
        _timer = new Timer(Reload, null, 0, 1000);
      });
      _logger.LogInformation("Открытие главной формы");
    }

    private async void Reload(object state)
    {
      if (_modelsGrid is not null)
      {
        await InvokeAsync(_modelsGrid.Reload);
      }
      InvokeAsync(StateHasChanged);
    }

    private async void GoCar()
    {
      _carThStopped = false;
      _thService.ResumeCarThread();
    }
    private async void StopCar()
    {
      _carThStopped = true;
      _thService.StopCarThread();
    }

    private async void GoDriver()
    {
      _driverThStopped = false;
      _thService.ResumeDriveThread();
    }
    private async void StopDriver()
    {
      _driverThStopped = true;
      _thService.StopDriveThread();
    }


  }
}