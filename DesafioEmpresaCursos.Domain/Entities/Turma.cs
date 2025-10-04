namespace DesafioEmpresaCursos.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; set; }
        public string? NumeroTurma { get; set; }
        public int AnoLetivo { get; set; }
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}
