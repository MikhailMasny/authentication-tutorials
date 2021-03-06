using Masny.IdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Masny.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //var connectionString = _config.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(config =>
            {
                //config.UseSqlServer(connectionString);
                config.UseInMemoryDatabase("Memory");
            });

            // AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            //var assembly = typeof(Startup).Assembly.GetName().Name;

            // https://docs.microsoft.com/en-us/archive/blogs/kaevans/using-powershell-with-certificates
            //var filePath = Path.Combine(env.ContentRootPath, "is_cert.pfx");
            //var certificate = new X509Certificate2(filePath, "password");

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                //        sql => sql.MigrationsAssembly(assembly));
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                //        sql => sql.MigrationsAssembly(assembly));
                //})
                // For X509Certificate2
                //.AddSigningCredential(certificate)
                // EF Core Setup
                .AddInMemoryApiResources(IdentityConfiguration.GetApis())
                .AddInMemoryIdentityResources(IdentityConfiguration.GetIdentityResources())
                .AddInMemoryClients(IdentityConfiguration.GetClients())
                // Migrate to v4
                //.AddInMemoryApiScopes(IdentityConfiguration.GetScopes())
                .AddDeveloperSigningCredential();

            //services.AddAuthentication()
            //    .AddFacebook(config =>
            //    {
            //        config.AppId = "";
            //        config.AppSecret = "";
            //    });

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
