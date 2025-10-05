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
        private readonly ITurmaService _turmaService;

        public AlunoService(IAlunoRepository alunoRepository, ITurmaService turmaService)
        {
            _alunoRepository=alunoRepository;
            _turmaService=turmaService;
        }

        public async Task<AlunoResponse> Create(AlunoRequest dto)
        {
            if (dto.TurmasId == null || !dto.TurmasId.Any())
            {
                throw new ArgumentException("O aluno deve ser matriculado em pelo menos uma turma.");
            }

            var turmas = await _turmaService.GetTurmasByIds(dto.TurmasId.ToList());

            var aluno = new Aluno
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                Cpf = dto.Cpf,            
                Turmas = turmas
            };
                        
            await _alunoRepository.Add(aluno);

            return new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email
            };
        }

        public async Task Delete(Guid id)
        {
            var aluno = await _alunoRepository.GetById(id);

            if (aluno == null)
            {
                throw new KeyNotFoundException("Aluno não encontrado.");
            }

            if (aluno.Turmas != null && aluno.Turmas.Any())
            {
                throw new ArgumentException("O aluno não pode ser excluído pois está matriculado em uma ou mais turmas.");
            }
            
            await _alunoRepository.Delete(id);
        }

        public Task<IEnumerable<AlunoResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AlunoResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}