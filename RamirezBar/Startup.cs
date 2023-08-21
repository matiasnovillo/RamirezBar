using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RamirezBar.Areas.BasicCore.Interfaces;
using RamirezBar.Areas.CMSCore.Interfaces;
using RamirezBar.Areas.BasicCore.Services;
using RamirezBar.Areas.CMSCore.Services;
using RamirezBar.Library;
using System;
using SixLaborsCaptcha.Mvc.Core;
using RamirezBar.Areas.BasicCulture.Services;
using RamirezBar.Areas.BasicCulture.Interfaces;

namespace RamirezBar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();

            //JSON to TimeSpan configuration
            services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonToTimeSpan()));

            //JSON configuration to output field names in PascalCase. Example: "TestId" : 1 and not "testId" : 1
            services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddHttpContextAccessor();

            //Area: BasicCore
            services.AddScoped<IFailure, FailureService>();
            services.AddScoped<IParameter, ParameterService>();
            services.AddScoped<IVisitorCounter, VisitorCounterService>();
            //Area: BasicCulture
            services.AddScoped<ICity, CityService>();
            services.AddScoped<IProvince, ProvinceService>();
            services.AddScoped<ICountry, CountryService>();
            services.AddScoped<IPlanet, PlanetService>();
            services.AddScoped<ISex, SexService>();
            //Area: CMSCore
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IMenu, MenuService>();
            services.AddScoped<IRoleMenu, RoleMenuService>();
            services.AddScoped<IRole, RoleService>();
            //Session configuration
            services.AddMvc();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            //Captcha configuration
            services.AddSixLabCaptcha(x =>
            {
                x.DrawLines = 0;
                x.NoiseRate = 0;
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

            app.UseSession();

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
