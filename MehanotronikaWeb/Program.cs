using MehatronikaAplication;
using MehatronikaCore;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);
var confManager = builder.Configuration;

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCoreServices(confManager);
builder.Services.AddAplicationServices();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/date-.txt", rollingInterval: RollingInterval.Minute)
    .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var lifetime = app.Lifetime;
lifetime.ApplicationStarted.Register(() => Log.Information("Application started."));
lifetime.ApplicationStopping.Register(() => Log.Information("Application is stopping."));
lifetime.ApplicationStopped.Register(() => Log.Information("Application stopped."));

app.Run();
