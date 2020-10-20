using LinkMe.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LinkMe.Middlewares
{
    public class RequestForwardMiddleware
    {
        private readonly RequestDelegate next;

        public RequestForwardMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILinkData linkData, ILinkClickData linkClickData)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                // TODO dostań się do bazy i sprawdź czy jest link o podanym skrócie, jesli nie to zwróć 404
                // potem sprawdź czy termin jest ok -> przekieruj, jeśli nie pokaż planszę o wygaśnięciu linku
                var link = linkData.GetLinkByShortLink(context.Request.Path.ToString().Substring(1));
                if (link == null)
                {
                    await this.next(context);
                    return;
                }

                // TODO find out if the remote address is always ::1
                Console.WriteLine(context.Connection.RemoteIpAddress.ToString());
                Console.WriteLine(context.Request.Path);
                if (link.ValidTo >= DateTime.UtcNow)
                {
                    linkClickData.AddLinkClick(link.ID, context.Connection.RemoteIpAddress.ToString());
                    context.Response.Redirect(link.OriginalLink);
                    return;
                }
                else
                {
                    // TODO return expired link page
                    Console.WriteLine("halo");
                }

                await this.next(context);
            }
            else
            {
                await this.next(context);
            }
        }
    }
}
