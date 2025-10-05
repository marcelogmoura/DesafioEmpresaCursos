using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioEmpresaCursos.Infra.Repositories
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Aluno> GetById(Guid id)
        {
            return await DbSet
                .Include(a => a.Turmas) 
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
