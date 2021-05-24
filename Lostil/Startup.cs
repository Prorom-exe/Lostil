
using Lostil.Data;
using Lostil.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil
{
    public class Startup
    {
        private IConfigurationRoot _conStr;
        public Startup(IHostEnvironment host)
        {
            _conStr = new ConfigurationBuilder().SetBasePath(host.ContentRootPath).AddJsonFile("dbset.json").Build();
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_conStr.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication();
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDbContext content = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var rolesManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await InitialNow.Initial(content, userManger, rolesManger);
            }
        }

    }
}
