using MehatronikaCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaCore.Context
{
  public class MehContext : DbContext
  {
    public MehContext(DbContextOptions<MehContext> options) : base(options) { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
  }
}
