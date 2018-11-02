namespace Flitesys.AirCloud.Core.Middleware.RequestId
{
    public class RequestIdOptions
    {
        private const string DefaultRequestIdHeader = "X-Request-Id";

        public string Header { get; set; } = DefaultRequestIdHeader;
        public bool IncludeInResponse { get; set; } = true;
    }
}