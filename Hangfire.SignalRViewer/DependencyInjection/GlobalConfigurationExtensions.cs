using Hangfire.Annotations;
using Hangfire.Dashboard;
using Hangfire.SignalRViewer.Pages;

namespace Hangfire.SignalRViewer.DependencyInjection
{
	public static class GlobalConfigurationExtensions
    {
        [PublicAPI]
        public static IGlobalConfiguration UseSignalRLogViewer(
            this IGlobalConfiguration configuration)
        {
            DashboardRoutes.Routes.AddRazorPage("/log-viewer",
                x => new LogViewer());

            NavigationMenu.Items.Add(page => new MenuItem("Log Viewer", page.Url.To("/log-viewer"))
            {
                Active = page.RequestPath.StartsWith("/log-viewer"),
            });

            return configuration;
        }
    }
}
