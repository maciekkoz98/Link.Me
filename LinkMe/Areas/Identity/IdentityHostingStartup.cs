using LinkMe.Areas.Identity.Data;
using LinkMe.Core.Entities;
using LinkMe.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LinkMe.Areas.Identity.IdentityHostingStartup))]

namespace LinkMe.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<LinkMeContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LinkMeContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LinkMeContext>();
                services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();
            });
        }
    }
}