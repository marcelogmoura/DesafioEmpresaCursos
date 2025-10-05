using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Infra.Data.Contexts;

namespace DesafioEmpresaCursos.Infra.Repositories
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
