using MehatronikaCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Services
{
  internal class DBInitService : IHostedService
  {
    private readonly IConfiguration _configuration;
    private readonly IDbContextFactory<MehContext> _contextFactory;

    public DBInitService(IConfiguration configuration, IDbContextFactory<MehContext> contextFactory)
    {
      _configuration = configuration;
      _contextFactory = contextFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      InitializeDatabase();
      return Task.CompletedTask;
    }

    private void InitializeDatabase()
    {
      using var context = _contextFactory.CreateDbContext();
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}
