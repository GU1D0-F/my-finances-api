using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyFinances.EntityFrameworkCore.Repositories.Reports.Export;
using MyFinances.EntityFrameworkCore.Repositories.Reports.Import;
using MyFinances.EntityFrameworkCore.Repositories.Spendings;
using MyFinances.Spendings;
using MyFinances.Users;
using ReportImportExport.Export;
using ReportImportExport.Import;
using System.Text;

namespace MyFinances.EntityFrameworkCore
{
    public static class DependenciesInjectorsEf
    {
        public static IServiceCollection AddDependenciesEntity(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIdentityDbContext(configuration);

            //services.AddScoped<IMetricsRepository, MetricsRepository>();
            //services.AddScoped<IEntradaSaidaRepository, EntradaSaidaRepository>();
            services.AddScoped<ISpendingRepository, SpendingRepository>();
            services.AddScoped<IReportImportRepository, ReportImportRepository>();
            services.AddScoped<IReportExportRepository, ReportExportRepository>();
            services.AddScoped<IReportImportLogRepository, ReportImportLogsRepository>();
            services.AddScoped<IReportExportLogsRepository, ReportExportLogsRepository>();

            return services;
        }

        private static IServiceCollection ConfigureIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyFinancesDbContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MyFinancesDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.User.AllowedUserNameCharacters += " ";
            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3A81F90D5A4E5E8A1C84E7D4B901D6BB67F4FD52415A84ECA7857D2B2E1A55F")),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = "glk-finances-identity",
                    ValidAudience = "glk-finances-apps",
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
