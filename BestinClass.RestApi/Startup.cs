using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestinClass.Core.Application_Service.Impl;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Helpers;
using BestinClass.Infrastructure.Data;
using BestinClass.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BestinClass.RestApi
{
    public class Startup
    {
        public IConfiguration _conf { get; }

        private IHostingEnvironment _env { get; set; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _conf = builder.Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<BestinClassContext>(
                    opt => opt.UseSqlite("Data Source=BiCDatabase.db"));
            }
            else if (_env.IsProduction())
            {
                services.AddDbContext<BestinClassContext>(
                    opt => opt
                        .UseSqlServer(_conf.GetConnectionString("defaultConnection")));
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtSecurityKey.Key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
            
            services.AddScoped<ITestEntityRepository, TestEntityRepository>();
            services.AddScoped<ITestEntityService, TestEntityService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IUserService, UserService>();



            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader()
                        .AllowAnyMethod());
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (IServiceScope scope = app.ApplicationServices.CreateScope())
                {
                   BestinClassContext ctx = scope.ServiceProvider.GetService<BestinClassContext>();
                   DBInitializer.SeedDB(ctx);
                }
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<BestinClassContext>();
                    ctx.Database.EnsureCreated();
                }
                app.UseHsts();
            }
            //app.UseCors("AllowSpecificOrigin");
            app.UseCors("AllowAnyOrigin");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}