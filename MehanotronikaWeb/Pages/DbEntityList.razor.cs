using MehatronikaAplication.Interfaces.Services;
using MehatronikaAplication.Model.ViewModels;
using MehatronikaAplication.Model;
using MehatronikaCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace MehanotronikaWeb.Pages
{
  public partial class DbEntityList
  {
    [Inject]
    public EntityList EntityList { get; set; }
    [Inject]
    public ILogger<Index> _logger { get; set; }

    private RadzenDataGrid<CarDriverViewModel> _modelsGrid;
    private Timer _timer;

    protected override void OnInitialized()
    {
      Task.Run(async () =>
      {
        _timer = new Timer(Reload, null, 0, 1000);
      });
      _logger.LogInformation("Открытие доп. формы");
    }

    private async void Reload(object state)
    {
      if (_modelsGrid is not null)
      {
        await InvokeAsync(_modelsGrid.Reload);
      }
      InvokeAsync(StateHasChanged);
    }
  }
}
