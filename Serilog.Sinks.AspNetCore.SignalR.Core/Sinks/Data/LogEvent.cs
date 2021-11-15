using System;
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Data
{
    public class LogEvent
    {
        public LogEvent()
        {
        }

        public LogEvent(Events.LogEvent logEvent, string renderedMessage)
        {
            Timestamp = logEvent.Timestamp;
            Exception = logEvent.Exception;
            MessageTemplate = logEvent.MessageTemplate.Text;
            Level = logEvent.Level;
            RenderedMessage = renderedMessage;
            Properties = new Dictionary<string, object>();

            foreach (var pair in logEvent.Properties)
            {
                Properties.Add(pair.Key, SignalRPropertyFormatter.Simplify(pair.Value));
            }
        }

        public DateTimeOffset Timestamp { get; set; }

        public string MessageTemplate { get; set; }

        public LogEventLevel Level { get; set; }

        public Exception Exception { get; set; }

        public string RenderedMessage { get; set; }

        public IDictionary<string, object> Properties { get; set; }
    }
}
