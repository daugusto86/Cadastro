using Cadastro.Core.DomainObjects;

namespace Cadastro.Cliente.Domain.Events
{
    public class NotificarCadastroEvent : DomainEvent
    {
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public NotificarCadastroEvent(Guid aggregateId, string cpf, string email) : base(aggregateId)
        {
            Cpf = cpf;
            Email = email;
        }
    }
}
