using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Masny.IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryApiResources(IdentityConfiguration.GetApis())
                //.AddInMemoryApiScopes(IdentityConfiguration.GetScopes()) // Migrate to v4
                .AddInMemoryClients(IdentityConfiguration.GetClients())
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
