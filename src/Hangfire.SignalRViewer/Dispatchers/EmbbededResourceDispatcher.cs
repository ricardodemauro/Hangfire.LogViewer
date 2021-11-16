using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Hangfire.SignalRViewer.Dispatchers
{
    public class EmbbededResourceDispatcher : IDashboardDispatcher
    {
        static readonly Assembly ThisAssembly = typeof(EmbbededResourceDispatcher).Assembly;

        public string ContentType { get; private set; }

        public string ResourceName { get; private set; }

        public TimeSpan Expires { get; private set; }

        public EmbbededResourceDispatcher(string contentType, string resourceName, TimeSpan timeSpan)
        {
            ContentType = contentType;
            ResourceName = resourceName;
            Expires = timeSpan;
        }

        public async Task Dispatch(DashboardContext context)
        {
            context.Response.ContentType = ContentType;
            context.Response.SetExpire(DateTimeOffset.UtcNow + Expires);
            await WriteResourceAsync(context);
        }

        private async Task WriteResourceAsync(DashboardContext context)
        {
            using (var stream = ThisAssembly.GetManifestResourceStream(ResourceName))
            {
                if (stream == null)
                {
                    context.Response.StatusCode = 404;
                }
                else
                {
                    await stream.CopyToAsync(context.Response.Body);
                }
            }
        }
    }
}
