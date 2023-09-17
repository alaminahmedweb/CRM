using ApplicationCore.Constants;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{

    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("con")));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                    options =>
                    {
                        options.Password.RequiredUniqueChars = 1;
                        options.Password.RequiredLength = 3;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            ////services.AddAuthentication(options =>
            ////{
            ////    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            ////    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            ////})
            ////    .AddJwtBearer(options =>
            ////    {
            ////        options.RequireHttpsMetadata = false;
            ////        options.SaveToken = true;
            ////        options.TokenValidationParameters = new TokenValidationParameters
            ////        {
            ////            ValidateIssuer = true,
            ////            ValidateAudience = true,
            ////            ValidateLifetime = true,
            ////            ValidateIssuerSigningKey = true,
            ////            ClockSkew = TimeSpan.Zero,

            ////            ValidIssuer = AuthorizationConstants.JWT_ISSUER,
            ////            ValidAudience = AuthorizationConstants.JWT_AUDIENCE,
            ////            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthorizationConstants.JWT_SECRET_KEY))
            ////        };
            ////    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });

            return services;
        }
    }
}
