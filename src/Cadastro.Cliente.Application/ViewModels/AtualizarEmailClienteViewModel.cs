using System.ComponentModel.DataAnnotations;

namespace Cadastro.Cliente.Application.ViewModels
{
    public class AtualizarEmailClienteViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
    }
}
