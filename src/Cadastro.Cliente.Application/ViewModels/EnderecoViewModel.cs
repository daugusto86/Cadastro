using System.ComponentModel.DataAnnotations;

namespace Cadastro.Cliente.Application.ViewModels
{
    public class EnderecoViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Logradouro { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Numero { get; set; }

        [StringLength(250, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Complemento { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Bairro { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Cep { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Cidade { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Estado { get; set; }

        public bool Principal { get; set; }

        [Required]
        public Guid IdCliente { get; set; }
    }
}
