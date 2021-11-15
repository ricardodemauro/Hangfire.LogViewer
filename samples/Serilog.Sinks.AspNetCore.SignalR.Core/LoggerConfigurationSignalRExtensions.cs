using Microsoft.AspNetCore.SignalR;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks;
using Serilog.Sinks.AspNetCore.SignalR.Core.Sinks.Interfaces;
using System;

namespace Serilog
{
    public static class LoggerConfigurationSignalRExtensions
    {


        public static LoggerConfiguration SignalR<THub, T>(
            this LoggerSinkConfiguration loggerConfiguration,
            IHubContext<THub, T> context,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = SignalRSink<THub, T>.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            string[] groupNames = null,
            string[] userIds = null,
            string[] excludedConnectionIds = null)
            where THub : Hub<T>
            where T : class, ISerilogHub
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (context == null) throw new ArgumentNullException(nameof(context));

            var defaultedPeriod = period ?? SignalRSink<THub, T>.DefaultPeriod;
            return loggerConfiguration.Sink(
                new SignalRSink<THub, T>(context, batchPostingLimit, defaultedPeriod, formatProvider, groupNames, userIds, excludedConnectionIds),
                restrictedToMinimumLevel);
        }

        public static LoggerConfiguration SignalRClient(
            this LoggerSinkConfiguration loggerConfiguration,
            string url,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = SignalRClientSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (url == null) throw new ArgumentNullException(nameof(url));

            var defaultedPeriod = period ?? SignalRClientSink.DefaultPeriod;
            return loggerConfiguration.Sink(
                new SignalRClientSink(url, batchPostingLimit, defaultedPeriod, formatProvider: formatProvider),
                restrictedToMinimumLevel);
        }
    }
}
