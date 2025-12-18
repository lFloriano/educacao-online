using EducacaoOnline.Conteudo.Domain.Repositories;
using EducacaoOnline.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Conteudo.Domain.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository ?? throw new ArgumentNullException(nameof(cursoRepository));
        }

        public async Task<Curso?> ObterPorIdAsync(Guid id)
        {
            return await _cursoRepository.ObterPorIdAsync(id);
        }

        public async Task<Curso> CadastrarCursoAsync(Curso curso)
        {
            if (curso == null)
                throw new ArgumentNullException(nameof(curso));

            if (await _cursoRepository.ExisteCursoComMesmoNomeAsync(curso.Nome))
                throw new DomainException("Já existe curso com o mesmo nome");

            _cursoRepository.Adicionar(curso);

            if (!await _cursoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao persistir o curso.");

            return curso;
        }

        public async Task<Aula> CadastrarAulaAsync(Aula aula)
        {
            if (aula == null)
                throw new ArgumentNullException(nameof(aula));

            var curso = await _cursoRepository.ObterPorIdAsync(aula.CursoId);

            if (curso == null)
                throw new DomainException("Curso não encontrado");

            curso.CadastrarAula(aula);
            _cursoRepository.AdicionarAula(aula);

            if (!await _cursoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao persistir a aula");

            return aula;
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            return await _cursoRepository.ObterTodosAsync();
        }

        public async Task<IEnumerable<Aula>?> ObterAulasPorCursoIdAsync(Guid id)
        {
            return await _cursoRepository.ObterAulasPorCursoIdAsync(id);
        }
    }
}
