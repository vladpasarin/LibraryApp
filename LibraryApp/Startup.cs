using AutoMapper;
using LibraryApp.Data;
using LibraryApp.IServices;
using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Repositories.Assets;
using LibraryApp.Repositories.IRepositories;
using LibraryApp.Repositories.Users;
using LibraryApp.Serialization;
using LibraryApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.ML;
using RecommendationSystem.DataModels;
using System;
using System.IO;
using EmailService;
using Microsoft.AspNetCore.Identity;
using LibraryApp.Entities;

namespace LibraryApp
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            // Registering services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetTagRepository, AssetTagRepository>();
            services.AddScoped<IAssetTagService, AssetTagService>();
            services.AddScoped<ICheckoutRepository, CheckoutRepository>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<IHoldRepository, HoldRepository>();
            services.AddScoped<IHoldService, HoldService>();
            services.AddScoped<ILibraryCardRepository, LibraryCardRepository>();
            services.AddScoped<ILibraryCardService, LibraryCardService>();
            services.AddScoped<IBookmarkRepository, BookmarkRepository>();
            services.AddScoped<IBookmarkService, BookmarkService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRatingService, RatingService>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            // Adding authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                            .ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddPredictionEnginePool<RatingData, RatingPrediction>()
                .FromFile(modelName: "BookRecommenderModel", 
                    filePath: "C:\\Users\\vlada\\Desktop\\myapp\\LibraryApp\\RecommendationSystem\\MLModels\\BookRecommenderModel.zip", 
                    watchForChanges: true);

            services.AddMvc();
            services.AddProgressiveWebApp();
            services.AddServiceWorker();

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfig>();
            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();
            /*services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LibraryDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(1));*/
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
