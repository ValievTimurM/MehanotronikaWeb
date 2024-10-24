using MehatronikaCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaCore
{
  public static class CoreDI
  {
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContextFactory<MehContext>(options =>
      {
        options.UseSqlite(configuration.GetConnectionString("DbCon"));
        options.UseSqlite(x => x.MigrationsHistoryTable("_EFMigrationHistory"));
      }, ServiceLifetime.Singleton);

      return services;
    }
  }
}
