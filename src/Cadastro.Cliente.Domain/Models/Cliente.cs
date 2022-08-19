using Cadastro.Cliente.Domain.Models.Validations;
using Cadastro.Core.DomainObjects;
using FluentValidation.Results;

namespace Cadastro.Cliente.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        // TODO:
        // criar propriedade de Inativo
        // criar value object para cpf e email
        // criar entidade filha endereço, vai ser uma lista
        // implementar paginação 

        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; set; }

        public Cliente(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = LimparCpf(cpf);
            Email = email;
            DataCadastro = DateTime.Now;
            Ativo = true;

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

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

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
