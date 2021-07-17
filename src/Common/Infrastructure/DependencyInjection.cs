using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Domain.Entities;
using CleanApplication.Infrastructure.Files;
using CleanApplication.Infrastructure.Helper;
using CleanApplication.Infrastructure.Identity;
using CleanApplication.Infrastructure.Persistence;
using CleanApplication.Infrastructure.Services;
using CleanApplication.Infrastructure.Services.CleanApplication.Infrastructure.Services;
using CleanApplication.Infrastructure.Services.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CleanApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                  .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                //opts.Password.RequiredLength = 8;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;

                //opts.SignIn.RequireConfirmedEmail = true;
            });
            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            services.AddTransient<IHttpClientHandler, HttpClientHandler>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IStringUtilities, StringUtilities>();
         


            //services.AddAuthentication()
            //    .AddIdentityServerJwt();
            // configure jwt authentication+

            var appSettingsSection = configuration.GetSection("JWT");
            var emailCredentialSection = configuration.GetSection("EmailCredential");
            var bankGatewayTypes = configuration.GetSection("BankGatewayType");
            services.Configure<JWT>(appSettingsSection);
            services.Configure<EmailApp>(emailCredentialSection);
            var jwtSettings = appSettingsSection.Get<JWT>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        UserManager<ApplicationUser> userService = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

                        // var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = context.Principal.Identity.Name;
                        var user = userService.FindByIdAsync(userId).Result;
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("admin", policy => policy.RequireRole("Administrator"));
            //});

            return services;
        }
    }
}