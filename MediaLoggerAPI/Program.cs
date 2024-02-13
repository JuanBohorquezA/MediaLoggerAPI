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
            EventLogger.InitConnection(configuration.GetSection(AppSettings.connectionMongo).Value, configuration.GetSection(AppSettings.DataBase).Value, configuration.GetSection(AppSettings.Colletion).Value);
            
            builder.Services.AddControllers();
            builder.Services.AddDependenciesInjectionConfig();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCorsDocumentation();
            builder.Services.AddAutoMapperConfig();
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}