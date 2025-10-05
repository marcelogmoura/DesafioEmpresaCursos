using DesafioEmpresaCursos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DesafioEmpresaCursos.Infra.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }                
        public DbSet<Aluno> Alunos { get; set; }        
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}