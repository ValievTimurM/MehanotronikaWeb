using MehatronikaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Model.ViewModels
{
  public class CarDriverViewModel
  {
    private CarDriverViewModel() { }
    public Guid Id { get; private set; }
    public string Car { get; set; }
    public string Driver { get; set; }
    public DateTime DateAdd { get; set; }

    public static CarDriverViewModel New(Car item)
        => new CarDriverViewModel()
        {
          Id = item.Id,
          Car = item.ModelName,
          DateAdd = item.DateAdd,
        };
    public static CarDriverViewModel New(Driver item)
        => new CarDriverViewModel()
        {
          Id = item.Id,
          Driver = item.Fio,
          DateAdd = item.DateAdd,
        };
  }
}
