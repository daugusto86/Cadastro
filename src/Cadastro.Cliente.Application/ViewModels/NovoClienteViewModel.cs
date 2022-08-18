using System.ComponentModel.DataAnnotations;

namespace Cadastro.Cliente.Application.ViewModels
{
    public class NovoClienteViewModel
    {
        [Required]
        [StringLength(255, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        public string Cpf { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
    }
}
