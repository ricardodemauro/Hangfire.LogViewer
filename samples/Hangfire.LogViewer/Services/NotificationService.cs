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
            Log.Debug("Running at {time} DEBUG", DateTime.Now);

            Log.Verbose("Running at {time} Verbose", DateTime.Now);

            Log.Information("Running at {time} Information", DateTime.Now);


            Log.Warning("Running at {time} Warning", DateTime.Now);

            Log.Fatal("Running at {time} Fatal", DateTime.Now);

            Log.Error("Running at {time} Error", DateTime.Now);
        }
    }
}
