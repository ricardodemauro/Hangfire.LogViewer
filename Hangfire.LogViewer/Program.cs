using Hangfire;
using Hangfire.Console;
using Hangfire.Console.Extensions;
using Hangfire.Console.Extensions.Serilog;
using Hangfire.LogViewer.Hubs;
using Hangfire.LogViewer.Services;
using Hangfire.SignalRViewer.DependencyInjection;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Interfaces;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();

var hubContext = app.Services.GetRequiredService<IHubContext<LogHub, ISerilogHub>>();

Log.Logger = new LoggerConfiguration()
    .Enrich.WithHangfireContext()
    .WriteTo.Console()
    .WriteTo.Hangfire()
    .WriteTo.SignalR<LogHub, ISerilogHub>(hubContext)
    .MinimumLevel.Override("Hangfire", Serilog.Events.LogEventLevel.Information)
    .CreateBootstrapLogger();


Configure(app, builder.Environment);



void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddHangfireServer(x =>
    {
        x.WorkerCount = 1;
    });

    var connectionString = configuration.GetConnectionString("Hangfire");

    services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        })
        .UseConsole()

        .UseSignalRLogViewer()
        );

    services.AddHangfireConsoleExtensions();

    services.AddSignalR();

    services.AddTransient<NotificationService>();
}

void Configure(WebApplication app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHangfireDashboard("/hangfire");
        endpoints.MapGet("/", () => "Hello World");
        endpoints.MapHub<LogHub>("/log-sink");
    });

    var n = app.Services.GetRequiredService<NotificationService>();
    RecurringJob.AddOrUpdate("easyjob", () => n.Run(), Cron.Minutely);

    app.Run();
}
