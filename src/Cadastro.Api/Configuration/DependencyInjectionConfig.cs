using Cadastro.Application.Interfaces;
using Cadastro.Application.Notificacoes;
using Cadastro.Application.Services;
using Cadastro.Domain.Interfaces;
using Cadastro.Infra.Context;
using Cadastro.Infra.Repository;

namespace Cadastro.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // entity context
            services.AddScoped<CadastroContext>();

            // repositories
            services.AddScoped<IClienteRepository, ClienteRepository>();

            // services
            services.AddScoped<IClienteService, ClienteService>();

            // notificador
            services.AddScoped<INotificador, Notificador>();

            // TODO
            // add HttpContextAccessor
            // add User
            // add SwaggerOptions

            return services;
        }
    }
}
