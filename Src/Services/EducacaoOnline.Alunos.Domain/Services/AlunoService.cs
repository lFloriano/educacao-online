using Azure.Core;
using EducacaoOnline.Alunos.Domain.Enums;
using EducacaoOnline.Alunos.Domain.Repositories;
using EducacaoOnline.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace EducacaoOnline.Alunos.Domain.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository ?? throw new ArgumentNullException(nameof(alunoRepository));
        }

        public async Task<Aluno?> ObterPorIdAsync(Guid id)
        {
            return await _alunoRepository.ObterPorIdAsync(id);
        }

        public async Task<Aluno?> ObterPorEmailAsync(string email)
        {
            return await _alunoRepository.ObterPorEmailAsync(email);
        }

        public async Task<Guid> CadastrarAlunoAsync(Aluno aluno)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno));

            _alunoRepository.Adicionar(aluno);

            if (!await _alunoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao persistir o aluno.");

            return aluno.Id;
        }

        public async Task<Matricula> MatricularAsync(Guid alunoId, Guid cursoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId)
                ?? throw new NotFoundException(nameof(Aluno), alunoId);

            var matricula = aluno.Matricular(cursoId);
            _alunoRepository.AdicionarMatricula(matricula);

            if (!await _alunoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao persistir a matrícula.");

            return matricula;
        }

        public async Task<Matricula> AtivarMatriculaAsync(Guid alunoId, Guid cursoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            var matricula = aluno.AtivarMatricula(cursoId);
            _alunoRepository.AtualizarMatricula(matricula);

            if (!await _alunoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao ativar a matrícula.");

            return matricula;
        }

        public async Task<AulaConcluida> RealizarAulaAsync(Guid alunoId, Guid cursoId, Guid aulaId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            var aulaConcluida = aluno.RealizarAula(aulaId, cursoId);
            _alunoRepository.Atualizar(aluno);

            if (!await _alunoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao persistir a conclusão da aula.");

            return aulaConcluida;
        }

        public async Task<Matricula> FinalizarCursoAsync(Guid alunoId, Guid cursoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            var matricula = aluno.FinalizarCurso(cursoId);
            _alunoRepository.Atualizar(aluno);

            if (!await _alunoRepository.UnitOfWork.Commit())
                throw new DbUpdateException("Falha ao persistir a finalização do curso.");

            return matricula;
        }

        public async Task<IEnumerable<Guid>> ObterCursosMatriculadosAsync(Guid alunoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            return aluno.ObterCursosMatriculados();
        }

        public async Task<IEnumerable<Guid>> ObterCursosConcluidosAsync(Guid alunoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            return aluno.ObterCursosConcluidos();
        }

        public async Task<int> ObterTaxaDeConclusaoDeCursosAsync(Guid alunoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            return aluno.ObterTaxaDeConclusaoDeCursos();
        }
    }
}