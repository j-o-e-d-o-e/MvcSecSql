using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcSecSql.Data.Data;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Migrations;

namespace MvcSecSql.Data
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VodContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                ));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<VodContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, VodContext db)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
//            DbInitializer.RecreateDatabase(db);
            DbInitializer.Initialize(db);
            app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}