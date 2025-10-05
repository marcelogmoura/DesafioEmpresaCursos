using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Infra.Data.Contexts;

namespace DesafioEmpresaCursos.Infra.Repositories
{
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
        public TurmaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
