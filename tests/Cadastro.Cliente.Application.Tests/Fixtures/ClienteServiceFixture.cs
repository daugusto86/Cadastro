using Bogus;
using Bogus.Extensions.Brazil;
using Cadastro.Cliente.Application.Services;
using Cadastro.Cliente.Application.ViewModels;
using Cadastro.Cliente.Domain.Models;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Cliente.Application.Tests.Fixtures
{
    [CollectionDefinition(nameof(ClienteAutoMockerCollection))]
    public class ClienteAutoMockerCollection : ICollectionFixture<ClienteServiceFixture>
    {

    }

    public class ClienteServiceFixture : IDisposable
    {
        public ClienteService ClienteService;
        public AutoMocker Mocker;
        private const string locale = "pt_BR";

        public ClienteService ObterClienteService()
        {
            Mocker = new AutoMocker();
            ClienteService = Mocker.CreateInstance<ClienteService>();

            return ClienteService;
        }

        public EnderecoViewModel GerarEnderecoViewModelValido()
        {
            var principal = new[] { true, false };
            var endereco = new Faker<EnderecoViewModel>(locale)
                .RuleFor(e => e.IdCliente, f => Guid.NewGuid())
                .RuleFor(e => e.Logradouro, f => f.Address.StreetName())
                .RuleFor(e => e.Numero, f => f.Address.BuildingNumber())
                .RuleFor(e => e.Complemento, f => f.Address.SecondaryAddress())
                .RuleFor(e => e.Bairro, f => "Centro")
                .RuleFor(e => e.Cep, f => f.Address.ZipCode())
                .RuleFor(e => e.Cidade, f => f.Address.City())
                .RuleFor(e => e.Estado, f => f.Address.State())
                .RuleFor(e => e.Principal, f => f.PickRandom(principal));
            
            return endereco;
        }

        public Endereco GerarEnderecoValido(bool principal)
        {
            var endereco = new Faker<Endereco>(locale)
                .CustomInstantiator(f => new Endereco(f.Address.StreetName(),
                    f.Address.BuildingNumber(),
                    f.Address.SecondaryAddress(),
                    "centro",
                    f.Address.ZipCode(),
                    f.Address.City(),
                    f.Address.State(),
                    Guid.NewGuid(),
                    principal));

            return endereco;
        }

        public NovoClienteViewModel GerarNovoClienteViewModelValido()
        {
            var cliente = new Faker<NovoClienteViewModel>(locale)
                .RuleFor(e => e.Nome, f => f.Name.FullName())
                .RuleFor(e => e.Cpf, f => f.Person.Cpf())
                .RuleFor(e => e.Email, f => f.Internet.Email());

            return cliente;
        }

        public NovoClienteViewModel GerarNovoClienteViewModelInvalido()
        {
            var cliente = new Faker<NovoClienteViewModel>(locale)
                .RuleFor(e => e.Nome, f => f.Name.FullName())
                .RuleFor(e => e.Cpf, f => "77777777777")
                .RuleFor(e => e.Email, f => "aaaaaaaa");

            return cliente;
        }

        public AtualizarEmailClienteViewModel GerarAtualizarEmailClienteViewModelValido()
        {
            return new Faker<AtualizarEmailClienteViewModel>(locale)
                .RuleFor(e => e.Id, Guid.NewGuid())
                .RuleFor(e => e.Email, f => f.Internet.Email());
        }

        public Domain.Models.Cliente GerarClienteValido()
        {
            return new Faker<Domain.Models.Cliente>(locale)
                .CustomInstantiator(f => new Domain.Models.Cliente(f.Name.FullName(),
                    f.Person.Cpf(), f.Internet.Email()));
        }

        public AtualizarClienteViewModel GerarAtualizarClienteViewModelValido()
        {
            return new Faker<AtualizarClienteViewModel>(locale)
                .RuleFor(e => e.Id, Guid.NewGuid())
                .RuleFor(e => e.Nome, f => f.Name.FullName());
        }

        public void Dispose()
        {
            
        }
    }
}
