using Microsoft.Extensions.Logging;
using System;

namespace Hangfire.LogViewer.Services
{
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run()
        {
            _logger.LogInformation("Running at {time}", DateTime.Now);
        }
    }
}
