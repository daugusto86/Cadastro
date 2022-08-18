﻿using Cadastro.Cliente.Domain.Models.Validations;
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
            cpf = LimparCpf(cpf);
            
            Nome = nome;
            Cpf = cpf;
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
        }

        public bool EhValida()
        {
            ValidationResult = new ClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private string LimparCpf(string cpf)
        {
            return cpf?.Trim().Replace(".", "").Replace("-", "");
        }
    }
}
