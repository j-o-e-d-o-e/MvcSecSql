using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcSecSql.Data.Data;
using MvcSecSql.Data.Data.Entities;
using MvcSecSql.Data.Services;
using MvcSecSql.Ui.Models.DtoModels;
using MvcSecSql.Ui.Services;
using MvcSecSql.UI.Repositories;

namespace MvcSecSql.Ui
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
            services.AddDbContext<VodContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<VodContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            //services.AddSingleton<IReadRepository, MockReadRepository>();
//            services.AddScoped<IReadRepository, SqlReadRepository>();
//            services.AddTransient<IDbReadService, DbReadService>();

            services.AddMvc();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Video, VideoDto>();

                cfg.CreateMap<Band, BandDto>()
                    .ForMember(dest => dest.BandName,
                        src => src.MapFrom(s => s.Name))
                    .ForMember(dest => dest.BandDescription,
                        src => src.MapFrom(s => s.Description))
                    .ForMember(dest => dest.BandImage,
                        src => src.MapFrom(s => s.BandImage));

                cfg.CreateMap<AlbumInfo, AlbumInfoDto>()
                    .ForMember(dest => dest.AlbumInfoUrl,
                        src => src.MapFrom(s => s.Url))
                    .ForMember(dest => dest.AlbumInfoTitle,
                        src => src.MapFrom(s => s.Title));

                cfg.CreateMap<Genre, GenreDto>()
                    .ForMember(dest => dest.GenreId, src =>
                        src.MapFrom(s => s.Id))
                    .ForMember(dest => dest.GenreTitle,
                        src => src.MapFrom(s => s.Title))
                    .ForMember(dest => dest.GenreDescription,
                        src => src.MapFrom(s => s.Description))
                    .ForMember(dest => dest.MarqueeImageUrl,
                        src => src.MapFrom(s => s.MarqueeImageUrl))
                    .ForMember(dest => dest.GenreImageUrl,
                        src => src.MapFrom(s => s.ImageUrl));

                cfg.CreateMap<Album, AlbumDto>()
                    .ForMember(dest => dest.AlbumTitle,
                        src => src.MapFrom(s => s.Title));
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}