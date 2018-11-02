using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Flitesys.AirCloud.Core.Middleware.RequestId
{
    public class RequestIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestIdOptions _options;

        public RequestIdMiddleware(RequestDelegate next, IOptions<RequestIdOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentException(nameof(options));
            }
            _next = next ?? throw new ArgumentException(nameof(next));
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(_options.Header, out StringValues requestId))
            {
                context.TraceIdentifier = requestId;
            }
            else
            {
                context.TraceIdentifier = Guid.NewGuid().ToString("N");
            }

            if (_options.IncludeInResponse)
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add(_options.Header, new[] { context.TraceIdentifier });
                    return Task.CompletedTask;
                });
            }
            return _next(context);
        }
    }
}