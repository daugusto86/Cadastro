using System.ComponentModel.DataAnnotations;

namespace Cadastro.Cliente.Application.ViewModels
{
    public class AtualizarClienteViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }
    }
}
