using Cadastro.Cliente.Application.AutoMapper;
using System.Reflection;

namespace Cadastro.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ClienteMappingProfile).GetTypeInfo().Assembly);
            
            return services;
        }
    }
}
