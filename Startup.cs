using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using SampleAPI.DataRepository;
using SampleAPI.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace SampleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public  string MyPolicy = "_myPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "My API",
                        Version = "v1"
                    });
            });
            services.AddMvc(options =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            });
            
            services.ConfigureApplicationCookie(options =>
            {
                //cookie settings
                options.Cookie.HttpOnly = true;
                //option.Cookie.Expiration=TImeSpan.FromDays(10)
                /*options.ExpireTimeSpan = TimeSpan.FromSeconds(600);
                options.LoginPath = "/Account/Login"; //If loginpath not set here then asp.net core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; //If loginpath not set here then asp.net core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;*/
            });

            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
            /* services.AddDbContext<CustomDbContext>(opts =>
              opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));*/
            services.AddDbContext<CustomDbContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            services.AddTransient<IReposiroty, ImpDataRepository>();
            services.AddSession();
            services.AddMemoryCache();
            /*services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });*/
            /*services.AddCors(options =>
            {
                options.AddPolicy(name: MyPolicy,
                    builder =>
                    {
                        builder.WithOrigins("http://example.com",
                                            "http://www.contoso.com",
                                            "http://cors1.azurewebsites.net",
                                            "http://cors3.azurewebsites.net",
                                            "http://localhost:44303",
                                            "http://localhost:3000",
                                            "http://localhost:100",
                                            "http://localhost:10",
                                            "http://localhost:1",
                                            "http://localhost:30")
                               .AllowAnyHeader()
                               .WithMethods("POST","PUT", "DELETE", "GET", "OPTIONS");
                    });
            });*/
            services.AddCors(o =>o.AddDefaultPolicy( builder =>
            {
                builder.
                AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            //services.AddCors();
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
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",  "My API V1");
                c.RoutePrefix = "";
            });
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseSession();
            //app.UseHttpsRedirection();

            

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseCors();
            /*app.UseCors(options=> options
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseCors(options => options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );*/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            /*app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=weatherforecast}/{action=Login}");
            });*/

            /* app.UseEndpoints(endpoints =>
            {
              endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller=WeatherForecast}/{action=Login}");
            });*/

           /* app.UseCors(options => options.WithOrigins("http://localhost:4200")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());*/
        }
    }
}
