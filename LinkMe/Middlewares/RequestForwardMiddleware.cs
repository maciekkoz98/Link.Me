using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Middlewares.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
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

        public async Task InvokeAsync(HttpContext context, ILinkRepository linkRepository, ILinkClickRepository linkClickRepository, IHttpClientFactory clientFactory)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                // TODO dostań się do bazy i sprawdź czy jest link o podanym skrócie, jesli nie to zwróć 404
                // potem sprawdź czy termin jest ok -> przekieruj, jeśli nie pokaż planszę o wygaśnięciu linku
                var link = await linkRepository.GetLinkByShortLinkAsync(context.Request.Path.ToString().Substring(1));
                if (link == null)
                {
                    await this.next(context);
                    return;
                }

                if (link.ValidTo >= DateTime.UtcNow)
                {
                    var ip = context.Connection.RemoteIpAddress.ToString();
                    if (ip.Equals("::1"))
                    {
                        ip = "77.252.193.249";
                    }

                    var httpClient = clientFactory.CreateClient("locationApi");
                    var response = await httpClient.GetAsync(ip);
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    using var streamReader = new StreamReader(responseStream);
                    using var jsonReader = new JsonTextReader(streamReader);
                    var serializer = new JsonSerializer();
                    var json = serializer.Deserialize<GeoApiResponse>(jsonReader);

                    var linkClick = new LinkClick()
                    {
                        LinkId = link.Id,
                        IPAddress = ip,
                        Country = json.CountryName,
                        CountryRegion = json.RegionName,
                        WhenClicked = DateTime.UtcNow,
                    };
                    await linkClickRepository.AddAsync(linkClick);
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
