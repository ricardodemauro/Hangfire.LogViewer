using Microsoft.AspNetCore.SignalR;
using Serilog.Sinks.PeriodicBatching;
using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Data;
using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.Sinks.AspNetCore.SignalR.Core.Sinks
{
    public class SignalRSink<THub, T> : PeriodicBatchingSink
        where THub : Hub<T>
        where T : class, ISerilogHub
    {
        readonly IFormatProvider _formatProvider;
        readonly IHubContext<THub, T> _hubContext;
        readonly string[] _groups;
        readonly string[] _userIds;
        readonly string[] _excludedConnectionIds;

        public const int DefaultBatchPostingLimit = 5;

        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        public SignalRSink(IHubContext<THub, T> context,
                           int batchPostingLimit,
                           TimeSpan period,
                           IFormatProvider formatProvider = null,
                           string[] groupNames = null,
                           string[] userIds = null,
                           string[] excludedConnectionIds = null)
            : base(batchPostingLimit, period)
        {
            _hubContext = context ?? throw new ArgumentNullException(nameof(context));

            _formatProvider = formatProvider;
            _groups = groupNames ?? Array.Empty<string>();
            _userIds = userIds ?? Array.Empty<string>();
            _excludedConnectionIds = excludedConnectionIds ?? Array.Empty<string>();
        }

        protected override void EmitBatch(IEnumerable<Events.LogEvent> events)
        {
            foreach (var logEvent in events)
                InnerEmit(new LogEvent(logEvent, logEvent.RenderMessage(_formatProvider)));
        }

        void InnerEmit(LogEvent logEvent)
        {
            if (logEvent == null) { throw new ArgumentNullException(nameof(logEvent)); }

            var targets = new List<ISerilogHub>();

            if (_groups.Any() == true)
            {
                targets.Add(_hubContext
                    .Clients
                    .Groups(_groups.Except(_excludedConnectionIds).ToArray())
                );
            }

            if (_userIds.Any() == true)
            {
                targets.Add(_hubContext
                    .Clients
                    .Users(_userIds.Except(_excludedConnectionIds).ToArray())
                );
            }

            if (!(_groups.Any() == true) && !(_userIds?.Any() == true))
            {
                targets.Add(_hubContext
                    .Clients
                    .AllExcept(_excludedConnectionIds)
                );
            }

            foreach (ISerilogHub target in targets)
            {
                ISerilogHub t = target;
                t.PushEventLog(logEvent);
            }
        }
    }
}
