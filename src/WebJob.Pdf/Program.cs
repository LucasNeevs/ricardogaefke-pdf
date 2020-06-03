﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RicardoGaefke.Data;
using RicardoGaefke.Domain;
using RicardoGaefke.Email;
using RicardoGaefke.Storage;
using RicardoGaefke.Pdf;

namespace RicardoGaefke.WebJob.Pdf
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var builder = new HostBuilder()
        .ConfigureWebJobs(b =>
        {
          b
            .AddAzureStorageCoreServices()
            .AddAzureStorage()
          ;
        })
        .ConfigureAppConfiguration(b =>
        {
          b.AddCommandLine(args);
        })
        .ConfigureLogging((context, b) =>
        {
          b.AddConsole();
          b.SetMinimumLevel(LogLevel.Information);
        })
        .ConfigureServices((context, services) =>
        {
          var Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build()
          ;

          services.Configure<Secrets.ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
          services.AddSingleton<IBlob, Blob>();
          services.AddSingleton<IQueue, Queue>();
          services.AddSingleton<IMyEmail, MyEmail>();
          services.AddSingleton<IPdf, Pdf>();
        })
        .UseConsoleLifetime()
      ;

      var host = builder.Build();

      using (host)
      {
        await host.RunAsync();
      }
    }
  }
}
