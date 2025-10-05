using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Dtos.Response;
using DesafioEmpresaCursos.Domain.Entities;

namespace DesafioEmpresaCursos.Domain.Interfaces.Services
{
    public interface ITurmaService
    {
        Task<TurmaResponse> Create(TurmaRequest dto);
        Task<TurmaResponse> GetById(Guid id);
        Task<IEnumerable<TurmaResponse>> GetAll();
        Task<TurmaResponse> Update(Guid id, TurmaRequest dto);        
        Task<string> Delete(Guid id);               
        Task<List<Turma>> GetTurmasByIds(List<Guid> ids);
    }
}
