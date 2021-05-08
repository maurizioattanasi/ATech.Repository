using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;

namespace ATech.Repository.CrashTestDummy
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }

        public static IDbConnection Connection { get; private set; }

        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            try
            {
                Log.Information("Starting Crash Test Dummy...");

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                var builder = new ConfigurationBuilder();

                if (env.ToLower() == "development")
                    builder.AddUserSecrets<Program>();

                Configuration = builder
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger();

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Information("Shutting down Crash Test Dummy");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    if ((args.Length > 0) && args[0] == "sqlite")
                    {
                        var connectionString = Configuration.GetConnectionString("SqlLiteConnectionString");
                        Connection = new SqliteConnection(connectionString);
                    }
                    else
                    {
                        var connectionString = Configuration.GetConnectionString("SqlServerConnectionString");
                        Connection = new SqlConnection(connectionString);
                    }

                    services.AddSingleton(Connection);

                    services.AddHostedService<Worker>();

                })
                .UseSerilog();
    }
}
