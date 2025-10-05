using DesafioEmpresaCursos.Domain.Dtos.Request;
using FluentValidation;

namespace DesafioEmpresaCursos.Domain.Validations
{
    public class AlunoRequestValidator : AbstractValidator<AlunoRequest>
    {
        public AlunoRequestValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .MaximumLength(100).WithMessage("O Nome deve ter no máximo 100 caracteres.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("O E-mail é obrigatório.")
                .EmailAddress().WithMessage("O E-mail é inválido.")
                .MaximumLength(100).WithMessage("O E-mail deve ter no máximo 100 caracteres.");

            RuleFor(a => a.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
                .Must(BeAValidCpf).WithMessage("O CPF é inválido.");

            RuleFor(a => a.TurmasId)
                .Must(t => t != null && t.Any()).WithMessage("O aluno deve ser matriculado em pelo menos uma turma.");
        }

        
        private bool BeAValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
                        
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
                        
            if (cpf.Length != 11) return false;
                        
            if (new string(cpf[0], 11) == cpf) return false;
                                
            return true;
        }
    }
}