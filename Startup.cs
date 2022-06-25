using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkBasics.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkBasics
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
            using (var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();

                if (!context.Settings.Any()) //ATTEMPTED TO USE BEAN CONFIGURATION, GAVE NULL ID ERROR? DONT KNOW WHY
                {
                    context.Settings.Add(new SettingsDataModel
                    {
                        Name = "BackgroundColor",
                        Value = "Red"
                    });
                }
                var settingsLocally = context.Settings.Local.Count();
                var settingsDatabase = context.Settings.Count();

                var firstLocal = context.Settings.Local.FirstOrDefault();
                var firstDatabase = context.Settings.FirstOrDefault();

                context.SaveChanges();
                settingsLocally = context.Settings.Local.Count();
                settingsDatabase = context.Settings.Count();

                firstLocal = context.Settings.Local.FirstOrDefault();
                firstDatabase = context.Settings.FirstOrDefault();

            }



            services.AddControllersWithViews();
            //services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
