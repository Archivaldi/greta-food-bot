using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GretaFoodCore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cmdArgs = Parser.Default.ParseArguments<CommandLineArguments>(args)
                .MapResult(
                    x => x,
                    x => null);

            if (cmdArgs == null)
                return;
            
            
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .CreateLogger();
            
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(cmdArgs).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(CommandLineArguments cmdArgs)
            => new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(x => x.AddSingleton(cmdArgs))
                .UseSetting(
                    WebHostDefaults.ApplicationKey,
                    typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
                .UseStartup<Startup>();
    }
}