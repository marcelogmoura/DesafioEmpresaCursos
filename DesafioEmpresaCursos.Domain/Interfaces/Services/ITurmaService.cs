using DesafioEmpresaCursos.Domain.Entities;

namespace DesafioEmpresaCursos.Domain.Interfaces.Services
{
    public interface ITurmaService
    {
        Task<List<Turma>> GetTurmasByIds(List<Guid> ids);
    }
}
