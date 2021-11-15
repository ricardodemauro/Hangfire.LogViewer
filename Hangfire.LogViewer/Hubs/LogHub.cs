using Microsoft.AspNetCore.SignalR;
using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Interfaces;

namespace Hangfire.LogViewer.Hubs
{
    public class LogHub : Hub<ISerilogHub>
    {
    }
}
