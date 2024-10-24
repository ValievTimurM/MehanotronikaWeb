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
  public class DriverService : IEntityService<Driver>
  {
    private readonly IRefRepositoryQuery _refRepositoryQuery;
    private readonly IRefRepositoryCommand _refRepositoryCommand;

    public DriverService(IRefRepositoryQuery refRepositoryQuery, IRefRepositoryCommand refRepositoryCommand)
    {
      _refRepositoryQuery = refRepositoryQuery;
      _refRepositoryCommand = refRepositoryCommand;
    }

    public async Task<Driver> GetLast()
        => await _refRepositoryQuery.FetchLastDriver();

    public async Task Add(Driver item)
        => await _refRepositoryCommand.AddDriver(item);
  }
}
