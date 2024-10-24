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
  public class RefRepositoryCommand : IRefRepositoryCommand
  {
    private readonly IDbContextFactory<MehContext> _contextFactory;
    public RefRepositoryCommand(IDbContextFactory<MehContext> contextFactory)
       => _contextFactory = contextFactory;

    #region Car
    public async Task AddCar(Car item)
    {
      using var db = _contextFactory.CreateDbContext();
      db.Cars.Add(item);
      await db.SaveChangesAsync();
    }
    #endregion

    #region Driver
    public async Task AddDriver(Driver item)
    {
      using var db = _contextFactory.CreateDbContext();
      db.Drivers.Add(item);
      await db.SaveChangesAsync();
    }
    #endregion
  }
}
