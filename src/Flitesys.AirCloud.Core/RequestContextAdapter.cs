using Microsoft.AspNetCore.Http;
using System;

namespace Flitesys.AirCloud.Core
{
    public class RequestContextAdapter : IRequestContextAdapter
    {
        private readonly IHttpContextAccessor _accessor;

        public RequestContextAdapter(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string RequestId =>
            _accessor.HttpContext?.Request.Headers["X-Request-ID"] ?? Guid.NewGuid().ToString("N");
    }
}