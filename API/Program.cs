using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

// run application
// dotnet watch run

namespace API
{
    public class Program
    {
        // Main() will be executed when application initally runs
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // create db based on migration.
            // get datacontext service. only need it while using this method.
            // anything inside this method will be cleaned up after the method is complete
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                // attempt to get db context and migrate db
                try 
                {
                    var context = services.GetRequiredService<DataContext>();
                    // create db if not already exist
                    context.Database.Migrate(); 
                } 
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>(); 
                    logger.LogError(ex, "An error occured during migration");
                }
            }
            // run application
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
