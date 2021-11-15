using Microsoft.AspNetCore.SignalR.Client;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogEvent = Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Data.LogEvent;

namespace Serilog.Sinks.AspNetCore.SignalR.Core.Sinks
{
    public class SignalRClientSink : PeriodicBatchingSink
    {
        readonly IFormatProvider _formatProvider;
        readonly HubConnection _connection;

        public const int DefaultBatchPostingLimit = 5;

        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        public SignalRClientSink(string url, int batchPostingLimit, TimeSpan period, IFormatProvider formatProvider)
            : base(batchPostingLimit, period)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            _formatProvider = formatProvider;

            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            _connection.Closed += OnConnectionClosed;

            _connection.StartAsync().GetAwaiter().GetResult();
        }

        private Task OnConnectionClosed(Exception arg)
        {
            return Task.CompletedTask;
        }

        protected override void EmitBatch(IEnumerable<Events.LogEvent> events)
        {
            foreach (var logEvent in events)
            {
                switch (_connection.State)
                {
                    case HubConnectionState.Connected:
                        _connection.InvokeAsync("PushEventLog", new LogEvent(logEvent, logEvent.RenderMessage(_formatProvider)));
                        break;
                    case HubConnectionState.Disconnected:
                        _connection.StartAsync();
                        break;
                }
            }
        }
    }
}
