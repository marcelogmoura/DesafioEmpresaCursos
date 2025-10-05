using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Dtos.Response;
using DesafioEmpresaCursos.Domain.Entities;

namespace DesafioEmpresaCursos.Domain.Interfaces.Services
{
    public interface IAlunoService
    { 
        AlunoResponse Create(AlunoRequest dto);
    }
}
