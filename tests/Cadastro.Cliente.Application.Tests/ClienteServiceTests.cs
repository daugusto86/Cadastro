using AutoMapper;
using Cadastro.Cliente.Application.Services;
using Cadastro.Cliente.Application.Tests.Fixtures;
using Cadastro.Cliente.Domain.Models;
using Moq;

namespace Cadastro.Cliente.Application.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteServiceFixture fixture;
        private readonly ClienteService service;

        public ClienteServiceTests(ClienteServiceFixture fixture)
        {
            this.fixture = fixture;
            service = fixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar endereço com sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_AdicionarEndereco_DeveExecutarComSucesso()
        {
            // Arrange
            var enderecoVm = fixture.GerarEnderecoViewModelValido();
            var endereco = new Mock<Endereco>();
            var cliente = new Mock<Domain.Models.Cliente>();

            fixture.Mocker.GetMock<IMapper>()
                .Setup(m => m.Map<Endereco>(enderecoVm))
                .Returns(endereco.Object);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(endereco.Object.IdCliente).Result)
                .Returns(cliente.Object);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.Atualizar(cliente.Object).Result)
                .Returns(true);

            // Act
            var sucesso = await service.AdicionarEndereco(enderecoVm);

            // Assert
            Assert.True(sucesso);
        }

        [Fact(DisplayName = "Adicionar endereço com falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_AdicionarEndereco_DeveExecutarComFalha()
        {
            // Arrange
            var enderecoVm = fixture.GerarEnderecoViewModelValido();
            var endereco = new Mock<Endereco>();
            Domain.Models.Cliente cliente = null;

            fixture.Mocker.GetMock<IMapper>()
                .Setup(m => m.Map<Endereco>(enderecoVm))
                .Returns(endereco.Object);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(endereco.Object.IdCliente).Result)
                .Returns(cliente);

            // Act
            var sucesso = await service.AdicionarEndereco(enderecoVm);

            // Assert
            Assert.False(sucesso);
        }


    }
}
