using Cadastro.Core.DomainObjects;
using Cadastro.Core.Interfaces;
using Cadastro.Core.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace Cadastro.Cliente.Application.Services
{
    public abstract class BaseService
    {
        private readonly INotificador notificador;

        public BaseService(INotificador notificador)
        {
            this.notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao(ValidationResult validationResult)
        {
            if (validationResult.IsValid) return true;

            Notificar(validationResult);
            return false;
        }
    }
}
