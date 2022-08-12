using Cadastro.Domain.Models.Validations.Documentos;
using FluentValidation;

namespace Cadastro.Domain.Models.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            //RuleFor(x => x.Cpf)
            //    .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            //    .Length(11).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres.");

            RuleFor(x => x.Cpf.Length).Equal(ValidacaoCpf.TamanhoCpf).WithMessage("O campo CPF precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(x => ValidacaoCpf.Validar(x.Cpf)).Equal(true).WithMessage("O CPF fornecido é inválido.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");
        }
    }
}
