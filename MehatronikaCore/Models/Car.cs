using MehatronikaCore.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaCore.Models
{
  public class Car : EntityBase
  {
    private Car(Guid id) : base(id) { }

    public string ModelName { get; set; } = "";

    public static Car New(string name)
        => new Car(Guid.NewGuid())
        {
          ModelName = name
        };
  }
}
