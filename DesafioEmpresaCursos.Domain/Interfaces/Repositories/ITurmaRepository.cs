using DesafioEmpresaCursos.Domain.Entities;

namespace DesafioEmpresaCursos.Domain.Interfaces.Repositories
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        Task<List<Turma>> GetByIds(List<Guid> ids);
    }
}
