using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace SmartPoint.Administrator.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerFormat(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            })
            .AddMvc()
            .AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddSwaggerGen(options =>
            {
                var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"SmartPoint.Administrator.Api {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = "Esta api serve os clients do sistema Smart Point."
                    });
                }
            });
        }

        public static void UseSwaggerFormat(this WebApplication app)
        {
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartPoint.Administrator v1");
            });
            //}
        }
    }
}
