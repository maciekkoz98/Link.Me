using LinkMe.Areas.Identity;
using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Data.InMemoryRepositories;
using LinkMe.Middlewares;
using LinkMe.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Headers;

namespace LinkMe
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILinkData, InMemoryLinkData>();
            services.AddSingleton<ILinkClickData, InMemoryLinkClickData>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddIdentityCore<User>()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>();
            services.Configure<AuthMessageSenderOptions>(this.Configuration);
            services.AddRazorPages();
            services.AddHttpClient("locationApi", client =>
            {
                client.BaseAddress = new Uri("https://freegeoip.app/json/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");

            var supportedCultures = new[] { "pl" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            app.UseStaticFiles();

            app.UseRouting();
            app.UseRequestForward();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
