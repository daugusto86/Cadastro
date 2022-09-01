using AutoMapper;
using Cadastro.Cliente.Application.Services;
using Cadastro.Cliente.Application.Tests.Fixtures;
using Cadastro.Cliente.Application.ViewModels;
using Cadastro.Cliente.Domain.Models;
using Cadastro.Core.DomainObjects;
using FluentValidation.Results;
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

        #region Testes referentes a cliente
        [Fact(DisplayName = "Obter Cliente por Id com Sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var clienteVm = new ClienteViewModel { Id = id };
            var cliente = new Mock<Domain.Models.Cliente>();

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(id).Result)
                .Returns(cliente.Object);

            fixture.Mocker.GetMock<IMapper>()
                .Setup(x => x.Map<ClienteViewModel>(cliente.Object))
                .Returns(clienteVm);

            fixture.Mocker.GetMock<IMapper>()
                .Setup(x => x.Map<List<EnderecoViewModel>>(cliente.Object.Enderecos))
                .Returns(new List<EnderecoViewModel>());

            // Act
            var result = await service.ObterPorId(id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Obter Cliente por Id com Falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_ObterPorId_DeveRetornarNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var clienteVm = new ClienteViewModel { Id = id };
            Domain.Models.Cliente cliente = null;

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(id).Result)
                .Returns(cliente);

            // Act
            var result = await service.ObterPorId(id);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var novoClienteVm = fixture.GerarNovoClienteViewModelValido();
            var cliente = new Domain.Models.Cliente(novoClienteVm.Nome, novoClienteVm.Cpf, novoClienteVm.Email);
            
            fixture.Mocker.GetMock<IMapper>()
                .Setup(x => x.Map<Domain.Models.Cliente>(novoClienteVm))
                .Returns(cliente);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.Adicionar(cliente).Result)
                .Returns(true);

            // Act
            var result = await service.Adicionar(novoClienteVm);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_Adicionar_DeveExecutarComFalha()
        {
            // Arrange
            var novoClienteVm = fixture.GerarNovoClienteViewModelInvalido();
            
            var erros = new List<ValidationFailure>
            {
                new ValidationFailure("Cpf", "Cpf Inválido"),
                new ValidationFailure("Email", "Email inválido")
            };
            var validationResult = new ValidationResult(erros);

            var cliente = new Mock<Domain.Models.Cliente>();
            
            // Moq só permite fazer setup de propriedade se ela for virtual
            // caso não ira gerar uma excessão
            cliente.Setup(x => x.ValidationResult).Returns(validationResult);
            
            fixture.Mocker.GetMock<IMapper>()
                .Setup(x => x.Map<Domain.Models.Cliente>(novoClienteVm))
                .Returns(cliente.Object);

            // Act
            var result = await service.Adicionar(novoClienteVm);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Cliente Atualizar E-mail Sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_AtualizarEmail_DeveExecutarComSucesso()
        {
            // Arrange
            var atualizarEmailVm = fixture.GerarAtualizarEmailClienteViewModelValido();
            var cliente = fixture.GerarClienteValido();

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(atualizarEmailVm.Id).Result)
                .Returns(cliente);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.Atualizar(cliente).Result)
                .Returns(true);

            // Act
            var result = await service.AtualizarEmail(atualizarEmailVm);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Cliente Atualizar E-mail Falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_AtualizarEmail_DeveExecutarComFalha()
        {
            // Arrange
            var atualizarEmailVm = fixture.GerarAtualizarEmailClienteViewModelValido();
            Domain.Models.Cliente cliente = null;

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(atualizarEmailVm.Id).Result)
                .Returns(cliente);

            // Act
            var result = await service.AtualizarEmail(atualizarEmailVm);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Cliente Ativar Sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_Ativar_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cliente = new Mock<Domain.Models.Cliente>();
            var clienteVm = new ClienteViewModel { Id = id };

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(id).Result)
                .Returns(cliente.Object);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.Atualizar(cliente.Object).Result)
                .Returns(true);

            fixture.Mocker.GetMock<IMapper>()
                .Setup(x => x.Map<ClienteViewModel>(cliente.Object))
                .Returns(clienteVm);

            // Act
            var resutl = await service.Ativar(id);

            // Assert
            Assert.NotNull(resutl);
        }

        [Fact(DisplayName = "Cliente Ativar Falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_Ativar_DeveRetornarNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            Domain.Models.Cliente cliente = null;
            
            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(id).Result)
                .Returns(cliente);

            // Act
            var resutl = await service.Ativar(id);

            // Assert
            Assert.Null(resutl);
        }

        [Fact(DisplayName = "Cliente Desativar Sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_Desativar_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cliente = new Mock<Domain.Models.Cliente>();
            var clienteVm = new ClienteViewModel { Id = id };

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(id).Result)
                .Returns(cliente.Object);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.Atualizar(cliente.Object).Result)
                .Returns(true);

            fixture.Mocker.GetMock<IMapper>()
                .Setup(x => x.Map<ClienteViewModel>(cliente.Object))
                .Returns(clienteVm);

            // Act
            var resutl = await service.Desativar(id);

            // Assert
            Assert.NotNull(resutl);
        }

        [Fact(DisplayName = "Cliente Desativar Falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_Desativar_DeveRetornarNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            Domain.Models.Cliente cliente = null;

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(id).Result)
                .Returns(cliente);

            // Act
            var resutl = await service.Desativar(id);

            // Assert
            Assert.Null(resutl);
        }
        #endregion

        #region Testes referentes a endereço
        [Fact(DisplayName = "Adicionar Endereço com Sucesso")]
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

        [Fact(DisplayName = "Adicionar Endereço com Falha")]
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

        [Fact(DisplayName = "Atualizar Endereço com Sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_AtualizarEndereco_DeveExecutarComSucesso()
        {
            // Arrange
            var enderecoVm = fixture.GerarEnderecoViewModelValido();
            var endereco = new Mock<Endereco>();
            var cliente = new Mock<Domain.Models.Cliente>();

            cliente.Object.AdicionarEndereco(endereco.Object);

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
            var sucesso = await service.AtualizarEndereco(enderecoVm);

            // Assert
            Assert.True(sucesso);
        }

        [Fact(DisplayName = "Atualizar Endereço com Falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_AtualizarEndereco_DeveExecutarComFalha()
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
            var sucesso = await service.AtualizarEndereco(enderecoVm);

            // Assert
            Assert.False(sucesso);
        }

        [Fact(DisplayName = "Remover Endereço com Sucesso")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_RemoverEndereco_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var endereco = fixture.GerarEnderecoValido(false);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterEnderecoPorId(id).Result)
                .Returns(endereco);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.RemoverEndereco(id).Result)
                .Returns(true);


            // Act
            var sucesso = await service.RemoverEndereco(id);


            // Assert
            Assert.True(sucesso);
        }

        [Fact(DisplayName = "Remover Endereço com Falha")]
        [Trait("Application", "Cliente Service")]
        public async Task ClienteService_RemoverEndereco_DeveExecutarComFalha()
        {
            // Arrange
            var id = Guid.NewGuid();
            var endereco = fixture.GerarEnderecoValido(true);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterEnderecoPorId(id).Result)
                .Returns(endereco);

            // Act
            var sucesso = await service.RemoverEndereco(id);


            // Assert
            Assert.False(sucesso);
        }
        #endregion
    }
}
