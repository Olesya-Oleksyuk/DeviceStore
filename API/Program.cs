using System;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
  public class Program
  {
    // DB is gonna be created when we start the App (no manual migrations are required)
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();


      // Getting access to our DataContext (because we are outside "services container" in the startup.cs)
      // using {} - to control over the lifetime of the instance of DataContext.
      using (var scope = host.Services.CreateScope())
      {
        // get our services to resolve dependencies from the scope.
        var services = scope.ServiceProvider;
        // register a service to log information out into the console
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        // handle any exceptions manually (since we're outside Startup.cs)
        try
        {
          var context = services.GetRequiredService<StoreContext>();
          // apply any pending (ожидаемые) migrations for the context to the DB & create DB if it doesn't exist
          await context.Database.MigrateAsync();
          // seed the data into the DB
          await StoreContextSeed.SeedAsync(context, loggerFactory);
        }
        catch (Exception ex)
        {
          // create the instance of the logger service (Program - the class that we wanna log against)
          var logger = loggerFactory.CreateLogger<Program>();
          logger.LogError(ex, "An error occured during migration");
        }
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                  webBuilder.UseStartup<Startup>();
                });
  }
}
