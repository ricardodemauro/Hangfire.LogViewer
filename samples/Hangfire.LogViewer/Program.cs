using Hangfire.Console.Extensions.Serilog;
using Hangfire.LogViewer.Hubs;
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

            host.Run();

        }

        static IWebHost CreateHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog()
            .Build();
    }
}