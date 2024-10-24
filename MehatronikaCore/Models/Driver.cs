using MehatronikaCore.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaCore.Models
{
  public class Driver : EntityBase
  {
    private Driver(Guid id) : base(id) { }

    public string Fio { get; set; } = "";

    public static Driver New(string name)
        => new Driver(Guid.NewGuid())
        {
          Fio = name,
        };
  }
}
