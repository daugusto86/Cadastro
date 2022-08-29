using Bogus;
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

        public ClienteService ObterClienteService()
        {
            Mocker = new AutoMocker();
            ClienteService = Mocker.CreateInstance<ClienteService>();

            return ClienteService;
        }

        public EnderecoViewModel GerarEnderecoViewModelValido()
        {
            var principal = new[] { true, false };
            var endereco = new Faker<EnderecoViewModel>("pt_BR")
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

        public void Dispose()
        {
            
        }
    }
}
