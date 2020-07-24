using Masny.Basic.AuthorizationRequirments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;

namespace Masny.Basic
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.MaxAge = new TimeSpan(0, 0, 10);
                    config.Cookie.Name = "BasicCookie";
                    config.LoginPath = "/Home/Authenticate";
                });

            services.AddAuthorization(config =>
            {
                // Default authorization policy
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAuthPolicy = defaultAuthBuilder
                //    .RequireAuthenticatedUser()
                //    .RequireClaim(ClaimTypes.Name)
                //    .Build();
                //config.DefaultPolicy = defaultAuthPolicy;

                //config.AddPolicy("Claim.Secret", policyBuilder =>
                //{
                //    policyBuilder.RequireClaim(ClaimTypes.Name);
                //});

                config.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, "Admin"));

                config.AddPolicy("Claim.Secret", policyBuilder =>
                {
                    policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.NameIdentifier));
                });
            });

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Who are you?
            app.UseAuthentication();

            // Are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
