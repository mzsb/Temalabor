using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Flatbuilder.DAL.Context;
using Flatbuilder.DAL.Entities.Authentication;
using Flatbuilder.DAL.Interfaces;
using Flatbuilder.DAL.Managers;
using Flatbuilder.WebAPI.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Flatbuilder.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            //ha van a db-ben many-to-many ne hivatkozzon korkorosen a json converter
            services.AddMvc().AddJsonOptions(
               json => json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            //appsettings.json-ban levo FlatbuilderContext nevu connection string-gel csatlakozzon adatbazishoz
            services.AddDbContext<FlatbuilderContext>(o => o.UseSqlServer(Configuration.GetConnectionString(nameof(FlatbuilderContext))));
            //automapper hozzáadása
            services.AddSingleton<IMapper>(MapperConfig.Configure());
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IRoomManager, RoomManager>();
            services.AddTransient<ICostumerManager, CostumerManager>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                      .AddEntityFrameworkStores<FlatbuilderContext>()
                      .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                   
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(
                options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
                );
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
        }
    }
}
