using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shapees.Models;
using Shapees.Web;
using Shapees.Models.TestModels;

using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Shapees.Models.DatabaseModel;

namespace Shapees
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            // Add framework services.  
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();

            services.AddSession();

            var sqlconnection = @"Server=(localdb)\mssqllocaldb;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<ShapeesDB>(dbcontextoption => dbcontextoption.UseSqlServer(sqlconnection));
            services.AddDbContext<testMasterContext>(dbcontextoption => dbcontextoption.UseSqlServer(sqlconnection));
            services.AddDbContext<masterContext>(dbcontextoption => dbcontextoption.UseSqlServer(sqlconnection));

            //Add service for accessing current HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging")); 
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            AppHttpContext.Services = app.ApplicationServices;

            app.UseSession();
            //Use in Controllers:
            //var data = new byte[] { 1, 2, 3, 4 };
            //HttpContext.Session.Set("key", data); // store byte array
            //HttpContext.Session.TryGetValue("key", out data); // read from session
            //HttpContext.Session.SetString("test", "data as string"); // store string
            //HttpContext.Session.SetInt32("number", 4711); // store int

            //int? number = HttpContext.Session.GetInt32("number");

        }
    }
}
