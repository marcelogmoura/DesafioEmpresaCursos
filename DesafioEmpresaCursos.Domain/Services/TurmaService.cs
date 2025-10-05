using DesafioEmpresaCursos.Domain.Dtos.Request;
using DesafioEmpresaCursos.Domain.Dtos.Response;
using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace DesafioEmpresaCursos.Domain.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        // ----------------------------------------------------
        // C - CREATE
        // ----------------------------------------------------
        public async Task<TurmaResponse> Create(TurmaCreateRequest dto)
        {
            var turma = new Turma
            {
                Id = Guid.NewGuid(),
                NumeroTurma = dto.NumeroTurma,
                AnoLetivo = dto.AnoLetivo
            };

            // Futuramente, adicionaremos a validação de formato (FluentValidation) aqui.

            await _turmaRepository.Add(turma);

            return new TurmaResponse
            {
                Id = turma.Id,
                NumeroTurma = turma.NumeroTurma,
                AnoLetivo = turma.AnoLetivo
            };
        }

        // ----------------------------------------------------
        // R - READ
        // ----------------------------------------------------
        public async Task<TurmaResponse> GetById(Guid id)
        {
            var turma = await _turmaRepository.GetById(id);

            if (turma == null)
            {
                throw new KeyNotFoundException($"Turma com Id {id} não encontrada.");
            }

            return new TurmaResponse
            {
                Id = turma.Id,
                NumeroTurma = turma.NumeroTurma,
                AnoLetivo = turma.AnoLetivo
            };
        }

        public async Task<IEnumerable<TurmaResponse>> GetAll()
        {
            var turmas = await _turmaRepository.GetAll();

            return turmas.Select(t => new TurmaResponse
            {
                Id = t.Id,
                NumeroTurma = t.NumeroTurma,
                AnoLetivo = t.AnoLetivo
            }).ToList();
        }

        // ----------------------------------------------------
        // U - UPDATE (Implementação básica)
        // ----------------------------------------------------
        public async Task<TurmaResponse> Update(Guid id, TurmaRequest dto)
        {
            var turma = await _turmaRepository.GetById(id);

            if (turma == null)
            {
                throw new KeyNotFoundException($"Turma com Id {id} não encontrada para atualização.");
            }

            // Aplica as alterações
            turma.NumeroTurma = dto.NumeroTurma;
            turma.AnoLetivo = dto.AnoLetivo;

            await _turmaRepository.Update(turma);

            return new TurmaResponse
            {
                Id = turma.Id,
                NumeroTurma = turma.NumeroTurma,
                AnoLetivo = turma.AnoLetivo
            };
        }

        public async Task<string> Delete(Guid id)
        {
            var turma = await ((TurmaRepository)_turmaRepository).GetByIdIncludingAlunos(id);

            if (turma == null)
            {
                throw new KeyNotFoundException($"Turma com Id {id} não encontrada.");
            }

            
            if (turma.Alunos != null && turma.Alunos.Any())
            {
                throw new ArgumentException("A turma não pode ser excluída pois possui alunos matriculados.");
            }

            var numeroTurma = turma.NumeroTurma;
            await _turmaRepository.Delete(id);

            return numeroTurma;
        }

        // ----------------------------------------------------
        // MÉTODO INTERNO (Já definido)
        // ----------------------------------------------------
        public async Task<List<Turma>> GetTurmasByIds(List<Guid> ids)
        {
            // ... (implementação existente)
            // OBS: O código GetTurmasByIds no TurmaRepository já faz o .Include(t => t.Alunos),
            // então não precisamos nos preocupar com a inclusão aqui.

            var turmas = await _turmaRepository.GetByIds(ids);

            // Validação: Checar se todas as turmas solicitadas foram encontradas
            if (turmas.Count != ids.Count)
            {
                var turmasNaoEncontradas = ids.Except(turmas.Select(t => t.Id)).ToList();
                throw new ArgumentException($"Algumas turmas não foram encontradas: {string.Join(", ", turmasNaoEncontradas)}");
            }

            // REGRA DE NEGÓCIO: Uma turma não pode ter mais de 5 alunos
            var turmasComCapacidadeExcedida = turmas
                .Where(t => t.Alunos.Count >= 5)
                .Select(t => t.NumeroTurma)
                .ToList();

            if (turmasComCapacidadeExcedida.Any())
            {
                throw new ArgumentException($"As seguintes turmas já atingiram o limite de 5 alunos: {string.Join(", ", turmasComCapacidadeExcedida)}");
            }

            return turmas;
        }
    }
}