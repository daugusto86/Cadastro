using Cadastro.Api.Extensions;
using Cadastro.Cliente.Domain.Events;
using Cadastro.Cliente.Domain.Interfaces;
using Cadastro.Cliente.Infra.Data.Context;
using Cadastro.Cliente.Infra.Data.Repository;
using Cadastro.Cliente.Infra.Messages;
using Cadastro.Core.Interfaces;
using Cadastro.Core.Mediator;
using Cadastro.Core.Notificacoes;
using MediatR;

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

            // app services
            services.AddScoped<Cliente.Application.Interfaces.IClienteService, Cliente.Application.Services.ClienteService>();

            // notificador
            services.AddScoped<INotificador, Notificador>();

            // domain services
            services.AddScoped<IClienteService, Cliente.Domain.Services.ClienteService>();

            //domain events
            services.AddScoped<INotificationHandler<NotificarCadastroEvent>, ClienteEventHandler>();

            // mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // infra messages services
            services.AddScoped<IEmailService, EmailService>();

            // context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // asp net user extension
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            // TODO
            // add SwaggerOptions

            return services;
        }
    }
}
