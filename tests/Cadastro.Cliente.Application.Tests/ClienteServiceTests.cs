using AutoMapper;
using Cadastro.Cliente.Application.Services;
using Cadastro.Cliente.Application.Tests.Fixtures;
using Cadastro.Cliente.Application.ViewModels;
using Cadastro.Cliente.Domain.Models;
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
        [Fact(DisplayName = "Cliente Obter por Id com Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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
            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(id), Times.Once);
        }

        [Fact(DisplayName = "Cliente Obter por Id com Falha")]
        [Trait("Categoria", "Cliente Service")]
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
            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(id), Times.Once);
        }

        [Fact(DisplayName = "Cliente Adicionar com Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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
            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.Adicionar(It.IsAny<Domain.Models.Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Adicionar com Falha")]
        [Trait("Categoria", "Cliente Service")]
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

        [Fact(DisplayName = "Cliente Atualizar com Sucesso")]
        [Trait("Categoria", "Cliente Service")]
        public async Task ClienteService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var clienteVm = fixture.GerarAtualizarClienteViewModelValido();
            var cliente = new Mock<Domain.Models.Cliente>();

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(clienteVm.Id))
                .Returns(Task.FromResult(cliente.Object));

            cliente.Setup(x => x.ValidationResult).Returns(new ValidationResult());

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.Atualizar(cliente.Object))
                .Returns(Task.FromResult(true));

            // Act
            var result = await service.Atualizar(clienteVm);

            // Assert
            Assert.True(result);
            
            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
            
            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.Atualizar(It.IsAny<Domain.Models.Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Atualizar com Falha")]
        [Trait("Categoria", "Cliente Service")]
        public async Task ClienteService_Atualizar_DeveExecutarComFalha()
        {
            // Arrange
            var clienteVm = fixture.GerarAtualizarClienteViewModelValido();
            Domain.Models.Cliente cliente = null;

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterPorId(clienteVm.Id))
                .Returns(Task.FromResult(cliente));

            // Act
            var result = await service.Atualizar(clienteVm);

            // Assert
            Assert.False(result);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Atualizar E-mail Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.Atualizar(It.IsAny<Domain.Models.Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Atualizar E-mail Falha")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Ativar Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.Atualizar(It.IsAny<Domain.Models.Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Ativar Falha")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Desativar Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.Atualizar(It.IsAny<Domain.Models.Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Cliente Desativar Falha")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
        }
        #endregion

        #region Testes referentes a endereço
        [Fact(DisplayName = "Endereço Obtrer por Id Sucesso")]
        [Trait("Categoria", "Cliente Service")]
        public async Task ClienteService_ObterEnderecoPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var endereco = new Mock<Endereco>();

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterEnderecoPorId(id))
                .Returns(Task.FromResult(endereco.Object));

            fixture.Mocker.GetMock<IMapper>()
                .Setup(x => x.Map<EnderecoViewModel>(endereco.Object))
                .Returns(new EnderecoViewModel());
                

            // Act
            var result = await service.ObterEnderecoPorId(id);

            // Assert
            Assert.NotNull(result);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterEnderecoPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Obtrer por Id Falha")]
        [Trait("Categoria", "Cliente Service")]
        public async Task ClienteService_ObterEnderecoPorId_DeveRetornarNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            Endereco endereco = null;

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterEnderecoPorId(id))
                .Returns(Task.FromResult(endereco));

            // Act
            var result = await service.ObterEnderecoPorId(id);

            // Assert
            Assert.Null(result);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterEnderecoPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Adicionar com Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.Atualizar(It.IsAny<Domain.Models.Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Adicionar com Falha")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Atualizar com Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.Atualizar(It.IsAny<Domain.Models.Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Atualizar com Falha")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Remover com Sucesso")]
        [Trait("Categoria", "Cliente Service")]
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterEnderecoPorId(It.IsAny<Guid>()), Times.Once);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.RemoverEndereco(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Remover com Falha")]
        [Trait("Categoria", "Cliente Service")]
        public async Task ClienteService_RemoverEndereco_FalhaEnderecoPrincipalNaoPodeSerRemovido()
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

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterEnderecoPorId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Endereço Remover com Falha")]
        [Trait("Categoria", "Cliente Service")]
        public async Task ClienteService_RemoverEndereco_DeveExecutarComFalhaEnderecoNaoEncontrado()
        {
            // Arrange
            var id = Guid.NewGuid();
            Endereco endereco = null;

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Setup(x => x.ObterEnderecoPorId(id).Result)
                .Returns(endereco);

            // Act
            var sucesso = await service.RemoverEndereco(id);


            // Assert
            Assert.False(sucesso);

            fixture.Mocker.GetMock<Domain.Interfaces.IClienteService>()
                .Verify(x => x.ObterEnderecoPorId(It.IsAny<Guid>()), Times.Once);
        }
        #endregion
    }
}
