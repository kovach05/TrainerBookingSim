using System.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogic;
using BusinessLogic.Interface;
using BusinessLogic.Services;
using Common;
using DataAccess;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebApi.Interfaces;
using WebApi.JWT;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
    
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            AddCustomServices(services, Configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddJwtAuthentication(IServiceCollection services, JWTSettings jwtSettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });
        }

        private void AddDataAccessServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>  
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        }

        private void AddBusinessLogicServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddHostedService<ExpiryCheckWorker>();
            services.AddSingleton(new ResourceManager("DataAccess.SqlQueries", typeof(StatisticsRepository).Assembly));
        }

        private void ConfigureJwtAndAutoMapper(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWTSettings").Get<JWTSettings>();
    
            if (jwtSettings == null)
            {
                throw new InvalidOperationException("JWTSettings cannot be null. Please check your configuration.");
            }
    
            services.AddSingleton(jwtSettings);
        }

        private void AddCustomServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureJwtAndAutoMapper(services, configuration);
            AddDataAccessServices(services, configuration);
            AddBusinessLogicServices(services);

            var jwtSettings = configuration.GetSection("JWTSettings").Get<JWTSettings>();
            AddJwtAuthentication(services, jwtSettings);

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();

        }
    }
}