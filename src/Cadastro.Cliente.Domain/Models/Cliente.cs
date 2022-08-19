using Cadastro.Cliente.Domain.Models.Validations;
using Cadastro.Core.DomainObjects;
using FluentValidation.Results;

namespace Cadastro.Cliente.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Cliente(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = LimparCpf(cpf);
            Email = email;
            DataCadastro = DateTime.Now;

            EhValida();
        }

        protected Cliente()
        {

        }

        public void MudarNome(string nome)
        {
            Nome = nome;
            EhValida();
        }

        public void MudarEmail(string email)
        {
            Email = email;
            EhValida();
        }

        public bool EhValida()
        {
            ValidationResult = new ClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private static string LimparCpf(string cpf)
        {
            return cpf?.Trim().Replace(".", "").Replace("-", "");
        }
    }
}
