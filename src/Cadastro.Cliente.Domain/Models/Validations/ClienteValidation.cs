using FluentValidation;

namespace Cadastro.Cliente.Domain.Models.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public static int NomeMinLength => 2;
        public static int NomeMaxLength => 255;
        public static string NomeNaoPreenchido => "O nome é obrigatório. Deve ser informado.";
        public static string NomeMaiorQuePermitido => $"O nome precisa ter entre {NomeMinLength} e {NomeMaxLength} caracteres";
        
        public ClienteValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(NomeNaoPreenchido)
                .Length(2, 255).WithMessage(NomeMaiorQuePermitido);
        }
    }
}
