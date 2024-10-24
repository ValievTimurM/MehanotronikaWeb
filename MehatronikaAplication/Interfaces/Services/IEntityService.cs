using MehatronikaCore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Interfaces.Services
{
  public interface IEntityService<T> where T : EntityBase
  {
    Task<T> GetLast();
    Task Add(T item);
  }
}
