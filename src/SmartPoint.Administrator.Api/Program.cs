using SmartPoint.Administrator.Api.Configuration;

namespace SmartPoint.Administrator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDBConfiguration(builder.Configuration);
            builder.Services.AddIdentityConfiguration(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerFormat(builder);

            builder.Services.AddDependencyInjection();

            var app = builder.Build();

            app.UseSwaggerFormat();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
