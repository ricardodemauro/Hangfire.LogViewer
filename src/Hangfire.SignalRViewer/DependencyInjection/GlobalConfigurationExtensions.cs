using Hangfire.Annotations;
using Hangfire.Dashboard;
using Hangfire.SignalRViewer.Dispatchers;
using Hangfire.SignalRViewer.Templater;
using Hangfire.SignalRViewer.Templater.Pages;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hangfire.SignalRViewer.Templater")] // The TestPrject will have access to the internal types and members

namespace Hangfire.SignalRViewer.DependencyInjection
{
    public static class GlobalConfigurationExtensions
    {


        internal static ViewerRenderOptions Options = new ViewerRenderOptions();

        [PublicAPI]
        public static IGlobalConfiguration UseSignalRLogViewer(
            this IGlobalConfiguration configuration,
            Action<ViewerRenderOptions> configure)
        {

            configure(Options);

            DashboardRoutes.Routes.AddRazorPage("/log-viewer", x => new LogViewer());

            NavigationMenu.Items.Add(page => new MenuItem("Log Viewer", page.Url.To("/log-viewer"))
            {
                Active = page.RequestPath.StartsWith("/log-viewer"),
            });

#if !DEBUG
            AddDashboardRouteToEmbeddedResource(
                "/signal-r-viewer/js/signalr.js",
                "application/javascript",
                "Hangfire.SignalRViewer.signalr.js");

            AddDashboardRouteToEmbeddedResource(
                "/signal-r-viewer/js/index.js",
                "application/javascript",
                "Hangfire.SignalRViewer.index.js");

            AddDashboardRouteToEmbeddedResource(
                "/signal-r-viewer/css/styles.css",
                "text/css",
                "Hangfire.SignalRViewer.styles.css");
#endif

            return configuration;
        }

        [PublicAPI]
        public static IGlobalConfiguration UseSignalRLogViewer(
            this IGlobalConfiguration configuration)
        {
            DashboardRoutes.Routes.AddRazorPage("/log-viewer", x => new LogViewer());

            NavigationMenu.Items.Add(page => new MenuItem("Log Viewer", page.Url.To("/log-viewer"))
            {
                Active = page.RequestPath.StartsWith("/log-viewer"),
            });

#if !DEBUG
            AddDashboardRouteToEmbeddedResource(
                "/signal-r-viewer/js/signalr.js",
                "application/javascript",
                "Hangfire.SignalRViewer.signalr.js");

            AddDashboardRouteToEmbeddedResource(
                "/signal-r-viewer/js/index.js",
                "application/javascript",
                "Hangfire.SignalRViewer.index.js");

            AddDashboardRouteToEmbeddedResource(
                "/signal-r-viewer/css/styles.css",
                "text/css",
                "Hangfire.SignalRViewer.styles.css");
#endif

            return configuration;
        }

        static void AddDashboardRouteToEmbeddedResource(
            string route,
            string contentType,
            string resourceName)
            => DashboardRoutes.Routes.Add(route, new EmbbededResourceDispatcher(contentType, resourceName, TimeSpan.FromDays(1)));
    }
}
