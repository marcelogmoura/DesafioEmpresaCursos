using Microsoft.EntityFrameworkCore;

namespace DesafioEmpresaCursos.Infra.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
                
        public DbSet<Aluno> Alunos { get; set; }

        
        public DbSet<Turma> Turmas { get; set; }

    }
}