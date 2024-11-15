

using DocSync.API.Helpers;
using DocSync.API.Infrastructure;
using DocSync.API.Mappings;
using DocSync.API.Repositories;
using DocSync.API.Repositories.Interfaces;
using DocSync.API.Services;
using DocSync.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DocSync.API.DependencyInjection
{
    public static class DependencyInjection
    {
        //public static IServiceCollection AddServices(this IServiceCollection services, IUnityContainer container)
        //{
        //    // Register services
        //    container.RegisterType<IDocumentInfoRepository, DocumentInfoRepository>();
        //    container.RegisterType<DocumentInfoService>();

        //    // Register Dapper configuration
        //    services.AddSingleton<DapperConfiguration>();

        //    return services;
        //}

        public static void AddDependencies(this IServiceCollection services, ConfigurationManager config)
        {
            // Registering Dapper
            services.AddSingleton<DapperConfiguration>();
            services.AddSingleton<MessageQueueConfiguration>();

            // Registering repositories and services
            services.AddScoped<IDocumentInfoRepository, DocumentInfoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();

            services.AddScoped<IDocumentInfoService, DocumentInfoService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDocumentService, DocumentService>();
            
            services.AddScoped<JwtHelper>();


            // Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
