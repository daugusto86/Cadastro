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
        // criar testes unitários
        // adicionar campo principal no endereco

        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        private readonly List<Endereco> enderecos = new();
        public IReadOnlyCollection<Endereco> Enderecos => enderecos;

        public Cliente(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = new Cpf(cpf);
            Email = new Email(email);
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
            Email = new Email(email);
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

        public void AdicionarEndereco(Endereco endereco)
        {
            if (enderecos.Any(x => x.Cep == endereco.Cep))
                throw new DomainException("CEP já cadastrado.");

            if (endereco.Principal)
                enderecos.ForEach(x => x.DesmarcarComoPrincipal());

            enderecos.Add(endereco);
        }

        public void RemoverEndereco(Guid id)
        {
            var endereco = enderecos.Find(x => x.Id == id);
            if (endereco == null)
                throw new DomainException("Endereço não encontrado.");
            
            enderecos.Remove(endereco);
        }

        public bool EhValida()
        {
            ValidationResult = new ClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
