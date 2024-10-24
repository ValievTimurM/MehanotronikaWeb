using MehatronikaAplication.Interfaces.Repositories;
using MehatronikaAplication.Interfaces.Services;
using MehatronikaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Services
{
  public class CarService : IEntityService<Car>
  {
    private readonly IRefRepositoryQuery _refRepositoryQuery;
    private readonly IRefRepositoryCommand _refRepositoryCommand;

    public CarService(IRefRepositoryQuery refRepositoryQuery, IRefRepositoryCommand refRepositoryCommand)
    {
      _refRepositoryQuery = refRepositoryQuery;
      _refRepositoryCommand = refRepositoryCommand;
    }

    public async Task<Car> GetLast()
        => await _refRepositoryQuery.FetchLastCar();

    public async Task Add(Car item)
        => await _refRepositoryCommand.AddCar(item);
  }
}
