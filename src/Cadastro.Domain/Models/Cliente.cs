using Cadastro.Core.DomainObjects;
using Cadastro.Cliente.Domain.Models.Validations;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastro.Cliente.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public Cliente(string nome, string cpf, string email, DateTime dataCadastro)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            DataCadastro = dataCadastro;
        }

        public Cliente()
        {

        }

        public void MudarNome(string nome)
        {
            Nome = nome;
        }

        public bool EhValida()
        {
            ValidationResult = new ClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
