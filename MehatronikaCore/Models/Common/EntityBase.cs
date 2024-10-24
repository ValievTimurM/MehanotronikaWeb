using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaCore.Models.Common
{
  public abstract class EntityBase
  {
    [Key]
    public Guid Id { get; set; }
    public DateTime DateAdd { get; set; }
    protected EntityBase(Guid id)
    {
      Id = id;
      DateAdd = DateTime.Now;
    }
  }
}
