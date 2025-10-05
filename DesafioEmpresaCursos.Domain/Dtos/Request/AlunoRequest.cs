namespace DesafioEmpresaCursos.Domain.Dtos.Request
{
    public class AlunoRequest
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
               
        public List<Guid>? TurmasId { get; set; } 
    }
}
