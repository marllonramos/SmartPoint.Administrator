using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartPoint.Administrator.Api.Configuration;
using SmartPoint.Administrator.Api.Configuration.Middlewares;
using SmartPoint.Administrator.Api.Extensions;
using System.Text;

namespace SmartPoint.Administrator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args).UseLogging();

            builder.Services.AddControllers();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddDBConfiguration(builder.Configuration);
            builder.Services.AddIdentityConfiguration(builder.Configuration);

            var appSettingsSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings!.Secret!);

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b =>
            {
                b.RequireHttpsMetadata = true;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidateIn,
                    ValidIssuer = appSettings.Issuer
                };
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerFormat(builder);

            builder.Services.AddLogging(builder.Configuration);

            builder.Services.AddDependencyInjection();

            var app = builder.Build();

            app.UseSwaggerFormat();

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
