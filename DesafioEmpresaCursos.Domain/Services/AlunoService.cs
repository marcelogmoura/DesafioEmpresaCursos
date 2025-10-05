using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Dtos.Response;
using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Domain.Interfaces.Services;

namespace DesafioEmpresaCursos.Domain.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository=alunoRepository;
        }

        public AlunoResponse Create(AlunoRequest dto)
        {
            var aluno = new Aluno
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                Cpf = dto.Cpf
            };

            _alunoRepository.Add(aluno);

            return new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email
            };

        }


    }
}
