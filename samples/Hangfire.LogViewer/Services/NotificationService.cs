using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Hangfire.LogViewer.Services
{
    public class NotificationService
    {
        public NotificationService()
        {
        }

        public void Run()
        {
            Log.Information("Running at {time}", DateTime.Now);
        }
    }
}
