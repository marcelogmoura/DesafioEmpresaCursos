using DesafioEmpresaCursos.Domain.Entities;
using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Domain.Interfaces.Services;

namespace DesafioEmpresaCursos.Domain.Services
{  
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<List<Turma>> GetTurmasByIds(List<Guid> ids)
        {   
            var turmas = await _turmaRepository.GetByIds(ids);
         
            if (turmas.Count != ids.Count)
            {
                var turmasNaoEncontradas = ids.Except(turmas.Select(t => t.Id)).ToList();
                                
                throw new ArgumentException($"Algumas turmas não foram encontradas: {string.Join(", ", turmasNaoEncontradas)}");
            }

            //REGRA DE NEGÓCIO: Uma turma não pode ter mais de 5 alunos
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