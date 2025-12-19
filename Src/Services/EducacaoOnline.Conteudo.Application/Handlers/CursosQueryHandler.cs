using AutoMapper;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.Conteudo.Application.Queries;
using EducacaoOnline.Conteudo.Domain.Repositories;
using EducacaoOnline.Core.Communication.Dtos;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Handlers
{
    public class CursosQueryHandler :
        IRequestHandler<ObterResumoCursoQuery, CursoResumoDto?>,
        IRequestHandler<ObterCursoPorIdQuery, CursoDto?>,
        IRequestHandler<ObterTodosOsCursosQuery, IEnumerable<CursoDto>>,
        IRequestHandler<ObterAulasPorCursoIdQuery, IEnumerable<AulaDto?>>,
        IRequestHandler<ObterAulaPorIdQuery, AulaDto?>,
        IRequestHandler<AulaPertenceAoCursoQuery, bool>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;

        public CursosQueryHandler(ICursoRepository cursoRepository, IMapper mapper)
        {
            _cursoRepository = cursoRepository;
            _mapper = mapper;
        }

        public async Task<CursoResumoDto?> Handle(ObterResumoCursoQuery request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.ObterPorIdAsync(request.CursoId);

            if (curso == null)
                return null;

            return new CursoResumoDto(curso.Id, curso.Nome, curso.ConteudoProgramatico.NumeroAulas, curso.Valor);
        }

        public async Task<CursoDto?> Handle(ObterCursoPorIdQuery request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.ObterPorIdAsync(request.CursoId);

            if (curso == null)
                return null;

            return _mapper.Map<CursoDto>(curso);
        }

        public async Task<IEnumerable<CursoDto>> Handle(ObterTodosOsCursosQuery request, CancellationToken cancellationToken)
        {
            var cursos = await _cursoRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<CursoDto>>(cursos);
        }

        public async Task<IEnumerable<AulaDto?>> Handle(ObterAulasPorCursoIdQuery request, CancellationToken cancellationToken)
        {
            var aulas = await _cursoRepository.ObterAulasPorCursoIdAsync(request.CursoId);
            return _mapper.Map<IEnumerable<AulaDto>>(aulas);
        }

        public async Task<AulaDto?> Handle(ObterAulaPorIdQuery request, CancellationToken cancellationToken)
        {
            var aulas = await _cursoRepository.ObterAulaPorIdAsync(request.AulaId);
            return _mapper.Map<AulaDto?>(aulas);
        }

        public async Task<bool> Handle(AulaPertenceAoCursoQuery request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.ObterPorIdAsync(request.CursoId);

            if (curso == null)
                return false;

            return curso.Aulas.Any(x => x.Id == request.AulaId);
        }
    }
}
