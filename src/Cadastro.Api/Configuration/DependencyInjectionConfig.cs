using Cadastro.Api.Extensions;
using Cadastro.Application.Interfaces;
using Cadastro.Application.Notificacoes;
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
            services.AddScoped<Application.Interfaces.IClienteService, Application.Services.ClienteService>();

            // notificador
            services.AddScoped<INotificador, Notificador>();

            // domain services
            services.AddScoped<Domain.Interfaces.IClienteService, Domain.Services.ClienteService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            // TODO
            // add SwaggerOptions

            return services;
        }
    }
}
