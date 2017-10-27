using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSO.Demo.Service.Context;
using SSO.Demo.Toolkits.Attribute;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;

namespace SSO.Demo.Sso
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new GolbalExceptionAttribute());
                options.Filters.Add(new GobalMvcModelValidAttribute());
            });

            services.AddSession();

            services.AddAuthentication(AuthenticationHelper.AuthenticationToken)
                .AddCookie(AuthenticationHelper.AuthenticationToken, options =>
                {
                    options.Cookie.Name = "ApplicationToken";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Expiration = DateTime.Now.AddMinutes(30).TimeOfDay;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.SlidingExpiration = true;
                });

            //注册数据库
            services.AddDbContext<SkyChenContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SkyChenDatabase")));

            services.AddService();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}

