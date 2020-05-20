//c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//Asp.Net dependencies
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

//queries
using analytics.Models;
using analytics.Queries;

namespace analytics
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
         public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy( //dev only
                    builder =>
                    {
                        builder.WithOrigins("*")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            services.AddControllersWithViews();
            string mySqlConnection = Configuration["DBInfo:ConnectionString"];
            System.Console.WriteLine(mySqlConnection);
            services.AddDbContext<AnalyticsContext>(options => options.UseMySql(mySqlConnection));
            services.AddScoped<AnalyticsQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(); //dev only

             app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
