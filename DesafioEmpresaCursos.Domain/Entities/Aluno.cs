namespace DesafioEmpresaCursos.Domain.Entities
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cpf{ get; set; }

        public List<Turma> Turmas { get; set; } = new List<Turma>();

    }
}
