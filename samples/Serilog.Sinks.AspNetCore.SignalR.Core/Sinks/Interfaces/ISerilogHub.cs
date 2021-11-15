using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Data;
using System.Threading.Tasks;

namespace Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Interfaces
{
    public interface ISerilogHub
    {
        Task PushEventLog(LogEvent message);
    }
}
