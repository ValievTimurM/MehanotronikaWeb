using MehatronikaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Interfaces.Repositories
{
  public interface IRefRepositoryCommand
  {
    Task AddCar(Car item);
    Task AddDriver(Driver item);
  }
}
