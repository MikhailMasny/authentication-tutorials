using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Masny.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var user = new IdentityUser("bob");
                userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                userManager.AddClaimAsync(user, new Claim("rc.garndma", "big.cookie"))
                    .GetAwaiter().GetResult();
                userManager.AddClaimAsync(user,
                    new Claim("rc.api.garndma", "big.api.cookie"))
                    .GetAwaiter().GetResult();

                //scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
                //    .Database.Migrate();

                //var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                //context.Database.Migrate();

                //if (!context.Clients.Any())
                //{
                //    foreach (var client in IdentityConfiguration.GetClients())
                //    {
                //        context.Clients.Add(client.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                //if (!context.IdentityResources.Any())
                //{
                //    foreach (var resource in IdentityConfiguration.GetIdentityResources())
                //    {
                //        context.IdentityResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                //if (!context.ApiResources.Any())
                //{
                //    foreach (var resource in IdentityConfiguration.GetApis())
                //    {
                //        context.ApiResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
