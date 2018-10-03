using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Flatbuilder.DAL.Context;
using Flatbuilder.WebAPI.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
            //ha van a db-ben many-to-many ne hivatkozzon korkorosen a json converter
            services.AddMvc().AddJsonOptions(
               json => json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            //appsettings.json-ban levo FlatbuilderContext nevu connection string-gel csatlakozzon adatbazishoz
            services.AddDbContext<FlatbuilderContext>(o => o.UseSqlServer(Configuration.GetConnectionString(nameof(FlatbuilderContext))));
            //automapper hozzáadása
            services.AddSingleton<IMapper>(MapperConfig.Configure());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
