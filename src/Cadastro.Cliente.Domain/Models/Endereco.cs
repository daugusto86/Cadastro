using Cadastro.Core.DomainObjects;

namespace Cadastro.Cliente.Domain.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public bool Principal { get; private set; }
        public Guid IdCliente { get; private set; }

        // EF Relation
        public Cliente Cliente { get; private set; }

        public Endereco(string logradouro, string numero, string complemento, string bairro, 
            string cep, string cidade, string estado, Guid idCliente, bool principal)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            IdCliente = idCliente;
            Principal = principal;
        }

        // EF Constructor
        protected Endereco() { }

        public void MarcarPrincipal() => Principal = true;
        public void DesmarcarComoPrincipal() => Principal = false;
    }
}
