using Microsoft.AspNetCore.Builder;

namespace LinkMe.Middlewares
{
    public static class RequestForwardMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestForward(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestForwardMiddleware>();
        }
    }
}
