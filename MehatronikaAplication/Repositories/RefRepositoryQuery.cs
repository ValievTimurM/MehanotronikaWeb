using MehatronikaAplication.Interfaces.Repositories;
using MehatronikaCore.Context;
using MehatronikaCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Repositories
{
  public class RefRepositoryQuery : IRefRepositoryQuery
  {
    private readonly IDbContextFactory<MehContext> _contextFactory;
    public RefRepositoryQuery(IDbContextFactory<MehContext> contextFactory)
       => _contextFactory = contextFactory;

    public async Task<Car> FetchLastCar()
    {
      using var db = _contextFactory.CreateDbContext();
      var car = await db.Cars.OrderByDescending(p => p.DateAdd).FirstOrDefaultAsync();
      return car;
    }

    public async Task<Driver> FetchLastDriver()
    {
      using var db = _contextFactory.CreateDbContext();
      var driver = await db.Drivers.OrderByDescending(p => p.DateAdd).FirstOrDefaultAsync();
      return driver;
    }
  }
}
