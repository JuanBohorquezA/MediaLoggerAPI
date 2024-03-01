using MediaLogger.Aplication.BL;
using MediaLogger.Domain.Variables;
using MediaLoggerAPI.App_Start;

namespace MediaLoggerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration.AddJsonFile("appsettings.json").Build();


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDependenciesInjectionConfig();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCorsDocumentation();
            builder.Services.AddAutoMapperConfig();

            var app = builder.Build();
            EventLogger.InitConnection(configuration.GetSection(AppSettings.connectionMongo).Value, configuration.GetSection(AppSettings.DataBase).Value, configuration.GetSection(AppSettings.Colletion).Value);






            app.UseCorsDocumentation();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();
        }
    }
}