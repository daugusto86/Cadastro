using Cadastro.Cliente.Domain.Models;
using Cadastro.Cliente.Domain.Models.Validations;
using Cadastro.Core.DomainObjects;

namespace Cadastro.Cliente.Domain.Tests
{
    public class ClienteTests
    {
        private Models.Cliente clienteValido;
        private Endereco enderecoValido;

        public ClienteTests()
        {
            clienteValido = new Models.Cliente("João", "052.461.250-17", "joao@abc.com");
            enderecoValido = new Endereco("Rua sem nome", "00", "casa", "centro", "17500-000",
                "São Paulo", "SP", clienteValido.Id, true);
        }

        [Fact(DisplayName = "Cliente com CPF Inválido")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_CpfInvalido_DeveRetornarException()
        {
            // Arrange && Act && Assert
            Assert.Throws<DomainException>(() => new Models.Cliente("João", "77777777777", "joao@abc.com"));
        }

        [Fact(DisplayName = "Cliente com e-mail Inválido")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_EmailInvalido_DeveRetornarException()
        {
            // Arrange && Act && Assert
            Assert.Throws<DomainException>(() => new Models.Cliente("João", "052.461.250-17", "joao.com"));
        }

        [Fact(DisplayName = "Cliente com nome Inválido")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_NomeInvalido_EntidadeDeveEstarInvalida()
        {
            // Arrange && Act
            var cliente = new Models.Cliente("", "052.461.250-17", "joao@abc.com");


            // Assert
            Assert.False(cliente.ValidationResult.IsValid);
            Assert.Contains(ClienteValidation.NomeNaoPreenchido, cliente.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Cliente mudar nome")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_MudarNome_DeveExecutarComSucesso()
        {
            // Arrange
            var novoNome = "José";

            // Act
            clienteValido.MudarNome(novoNome);

            // Assert
            Assert.Equal(novoNome, clienteValido.Nome);
            Assert.True(clienteValido.ValidationResult.IsValid);
        }

        [Fact(DisplayName = "Cliente mudar nome para um nome inválido")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_MudarNome_NomeInvalido_EntidadeDeveEstarInvalida()
        {
            // Arrange
            var novoNome = "";

            // Act
            clienteValido.MudarNome(novoNome);

            // Assert
            Assert.False(clienteValido.ValidationResult.IsValid);
            Assert.Contains(ClienteValidation.NomeNaoPreenchido, clienteValido.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Cliente mudar e-mail para um e-mail inválido")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_MudarEmail_EmailInvalido_DeveRetornarException()
        {
            // Arraange
            var novoEmail = "joao.com";


            // Act && Assert
            Assert.Throws<DomainException>(() => clienteValido.MudarEmail(novoEmail));
        }

        [Fact(DisplayName = "Cliente mudar e-mail")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_MudarEmail_DeveExecutarComSucesso()
        {
            // Arraange
            var novoEmail = "joao123@abc.com";


            // Act
            clienteValido.MudarEmail(novoEmail);

            // Assert
            Assert.True(clienteValido.ValidationResult.IsValid);
            Assert.Equal(novoEmail, clienteValido.Email.Endereco);
        }

        [Fact(DisplayName = "Cliente ativar")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_Ativar_DeveExecutarComSucesso()
        {
            // Arrange && Act
            clienteValido.Ativar();

            // Assert
            Assert.True(clienteValido.Ativo);
        }

        [Fact(DisplayName = "Cliente desativar")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_Desativar_DeveExecutarComSucesso()
        {
            // Arrange && Act
            clienteValido.Desativar();

            // Assert
            Assert.False(clienteValido.Ativo);
        }

        [Fact(DisplayName = "Cliente adicionar endereço")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_AdicionarEndereco_DeveExecutarComSucesso()
        {
            // Arrange && Act
            clienteValido.AdicionarEndereco(enderecoValido);

            // Assert
            Assert.True(clienteValido.Enderecos.Count > 0);
        }

        [Fact(DisplayName = "Cliente adicionar endereço existente")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_AdicionarEndereco_EnderecoJaExiste_DeveRetornarException()
        {
            // Arrange
            clienteValido.AdicionarEndereco(enderecoValido);

            // Act && Assert
            Assert.Throws<DomainException>(() => clienteValido.AdicionarEndereco(enderecoValido));
        }

        [Fact(DisplayName = "Cliente remover endereço")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_RemoverEndereco_DeveExecutarComSucesso()
        {
            // Arrange
            clienteValido.AdicionarEndereco(enderecoValido);

            // Act
            clienteValido.RemoverEndereco(enderecoValido.Id);

            // Assert
            Assert.True(!clienteValido.Enderecos.Any(x => x.Id == enderecoValido.Id));
        }

        [Fact(DisplayName = "Cliente remover endereço inexistente")]
        [Trait("Categoria", "Cliente Domain")]
        public void Cliente_RemoverEndereco_EnderecoInexistente_DeveRetornarException()
        {
            // Arrange && Act && Assert
            Assert.Throws<DomainException>(() => clienteValido.RemoverEndereco(enderecoValido.Id));
        }
    }
}
