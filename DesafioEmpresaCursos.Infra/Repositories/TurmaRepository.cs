using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioEmpresaCursos.Infra.Repositories
{
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
        public TurmaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Turma>> GetByIds(List<Guid> ids)
        { 
            return await DbSet
                .Where(t => ids.Contains(t.Id))
                .Include(t => t.Alunos)
                .ToListAsync();
        }

        public async Task<Turma> GetByIdIncludingAlunos(Guid id)
        {
            return await DbSet
                .Include(t => t.Alunos)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
