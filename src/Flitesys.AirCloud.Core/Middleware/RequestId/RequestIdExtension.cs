using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;

namespace Flitesys.AirCloud.Core.Middleware.RequestId
{
    public static class RequestIdExtension
    {
        public static IApplicationBuilder UseRequestId(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentException(nameof(app));
            }
            return app.UseMiddleware<RequestIdMiddleware>();
        }

        public static IApplicationBuilder UseRequestId(this IApplicationBuilder app, string header)
        {
            if (app == null)
            {
                throw new ArgumentException(nameof(app));
            }
            return app.UseRequestId(new RequestIdOptions { Header = header });
        }

        public static IApplicationBuilder UseRequestId(this IApplicationBuilder app, RequestIdOptions options)
        {
            if (app == null)
            {
                throw new ArgumentException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentException(nameof(options));
            }
            return app.UseMiddleware<RequestIdMiddleware>(Options.Create(options));
        }
    }
}