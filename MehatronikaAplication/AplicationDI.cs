using MehatronikaAplication.Interfaces.Repositories;
using MehatronikaAplication.Interfaces.Services;
using MehatronikaAplication.Model;
using MehatronikaAplication.Repositories;
using MehatronikaAplication.Services;
using MehatronikaAplication;
using MehatronikaCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MehatronikaAplication.ThTimer;
using Microsoft.Extensions.Hosting;

namespace MehatronikaAplication
{
  public static class AplicationDI
  {
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
      services.AddSingleton<EntityList>(EntityList.getInstance());
      services.AddSingleton<ThreadTimer>();

      services.AddHostedService<DBEntitiesBuildService>();
      services.AddHostedService<DBInitService>();

      services.AddTransient<IEntityService<Car>, CarService>();
      services.AddTransient<IEntityService<Driver>, DriverService>();
      services.AddTransient<IRefRepositoryCommand, RefRepositoryCommand>();
      services.AddTransient<IRefRepositoryQuery, RefRepositoryQuery>();
      services.AddSingleton<IThreadingService, ThreadingService>();

      return services;
    }
  }
}
