using Cadastro.Application.Interfaces;
using Cadastro.Application.Notificacoes;
using Cadastro.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Cadastro.Application.Services
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

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
