using Cadastro.Api.Extensions;
using Cadastro.Cliente.Application.Services;
using Cadastro.Cliente.Domain.Interfaces;
using Cadastro.Cliente.Domain.Services;
using Cadastro.Cliente.Infra.Data.Context;
using Cadastro.Cliente.Infra.Data.Repository;
using Cadastro.Core.Interfaces;
using Cadastro.Core.Notificacoes;

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
            services.AddScoped<Cliente.Application.Interfaces.IClienteService, Cadastro.Cliente.Application.Services.ClienteService>();

            // notificador
            services.AddScoped<INotificador, Notificador>();

            // domain services
            services.AddScoped<IClienteService, Cliente.Domain.Services.ClienteService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            // TODO
            // add SwaggerOptions

            return services;
        }
    }
}
