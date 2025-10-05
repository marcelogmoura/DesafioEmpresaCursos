using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Dtos.Response;
using DesafioEmpresaCursos.Domain.Entities;

namespace DesafioEmpresaCursos.Domain.Interfaces.Services
{
    public interface IAlunoService
    {        
        Task<AlunoResponse> Create(AlunoRequest dto);
        Task<AlunoResponse> GetById(Guid id);
        Task<IEnumerable<AlunoResponse>> GetAll();
        Task Delete (Guid id);
    }
}
