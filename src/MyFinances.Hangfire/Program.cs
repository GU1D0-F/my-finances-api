using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyFinances.Domain;
using MyFinances.EntityFrameworkCore;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDependenciesDomain();
builder.Services.AddDependenciesEntity(builder.Configuration);
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire")));
builder.Services.AddHangfireServer();

using IHost host = builder.Build();

await host.RunAsync();