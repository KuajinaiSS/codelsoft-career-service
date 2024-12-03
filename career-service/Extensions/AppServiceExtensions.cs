using career_service.Data;
using career_service.Repositories;
using career_service.Repositories.Interfaces;
using career_service.Services;
using career_service.Services.Interfaces;
using DotNetEnv;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace career_service.Extensions;

public static class AppServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            InitEnvironmentVariables();
            AddAutoMapper(services);
            AddServices(services);
            AddDbContext(services);
            AddUnitOfWork(services);
        }

        private static void InitEnvironmentVariables()
        {
            Env.Load();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IMapperService, MapperService>();
            services.AddScoped<ICareersService, CareerService>();
        } 

        private static void AddDbContext(IServiceCollection services)
        {
            var connectionUrl = Env.GetString("DB_CONNECTION") ?? throw new NullReferenceException("DB_CONNECTION is not defined in .env");
            Console.WriteLine("Iniciando conexion a la base de datos "+ connectionUrl);
            
            // var connectionUrl = "Server=localhost;Database=dbo;User Id=sa;Password=yourStrongPassword#1234;TrustServerCertificate=true";
            
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionUrl, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: System.TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    );
                });
            });
            Console.WriteLine("Conexion a la base de datos terminada");

        }

        private static void AddUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }
        
}