using Hangfire.Common;
using Hangfire.Console.Extensions.Serilog;
using Hangfire.LogViewer.Hubs;
using Hangfire.LogViewer.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Interfaces;

namespace Hangfire.LogViewer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            var host = CreateHost(args);

            var hubContext = host.Services.GetRequiredService<IHubContext<LogHub, ISerilogHub>>();

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithHangfireContext()
                .WriteTo.Console()
                .WriteTo.Hangfire()
                .WriteTo.SignalR<LogHub, ISerilogHub>(hubContext)

                .MinimumLevel.Override("Hangfire", Serilog.Events.LogEventLevel.Information)
                .CreateBootstrapLogger();

            var manager = host.Services.GetRequiredService<IRecurringJobManager>();
            manager.AddOrUpdate("some-id", Job.FromExpression(() => Easy()), "*/20 * * * * *");

            var notService = host.Services.GetRequiredService<NotificationService>();
            manager.AddOrUpdate("NotificationService", Job.FromExpression(() => Notify(notService)), Cron.Minutely());
            host.Run();
        }

        public static void Notify(NotificationService service)
        {
            service.Run();
        }

        public static void Easy()
        {
            Log.Information("Hello from Easy");
        }

        static IWebHost CreateHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            //.UseSerilog()
            .Build();
    }
}