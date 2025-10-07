using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Dtos.Response;
using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Domain.Interfaces.Services;
using System.Net.Mail;

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

            var turmasDuplicadas = dto.TurmasId
                .GroupBy(id => id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (turmasDuplicadas.Any())
            {
                throw new ArgumentException($"O aluno não pode se matricular mais de 1x na mesma turma. IDs duplicados encontrados: {string.Join(", ", turmasDuplicadas)}");
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

        public async Task<IEnumerable<AlunoResponse>> GetAll()
        {
            var alunos = await _alunoRepository.GetAll();

            return alunos.Select(aluno => new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email
            });
        }

        public async Task<AlunoResponse> GetById(Guid id)
        {
            var aluno = await _alunoRepository.GetById(id);

            if (aluno == null)
            {
                throw new KeyNotFoundException($"Aluno com Id {id} não encontrado.");
            }

            return new AlunoResponse
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email
            };
        }

        public async Task<AlunoResponse> Update(Guid id, AlunoUpdateRequest dto)
        {            
            var aluno = await _alunoRepository.GetById(id);

            if (aluno == null)
                throw new KeyNotFoundException($"Aluno com Id {id} não encontrado para atualização.");
                     
            // Atualiza Nome (apenas se for fornecido)
            if (!string.IsNullOrWhiteSpace(dto.Nome))
            {
                aluno.Nome = dto.Nome;
            }

            // Atualiza Email (apenas se for fornecido)
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                try
                {                    
                    var mailAddress = new MailAddress(dto.Email);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("O formato do e-mail fornecido é inválido.");
                }
                aluno.Email = dto.Email;
            }
                        
            await _alunoRepository.Update(aluno);

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