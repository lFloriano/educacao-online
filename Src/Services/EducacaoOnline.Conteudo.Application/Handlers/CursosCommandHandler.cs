using AutoMapper;
using EducacaoOnline.Conteudo.Application.Commands;
using EducacaoOnline.Conteudo.Application.Dtos;
using EducacaoOnline.Conteudo.Domain;
using EducacaoOnline.Conteudo.Domain.Services;
using EducacaoOnline.Conteudo.Domain.ValueObjects;
using MediatR;

namespace EducacaoOnline.Conteudo.Application.Handlers
{
    public class CursosCommandHandler :
        IRequestHandler<CadastrarCursoCommand, CursoDto>,
        IRequestHandler<CadastrarAulaCommand, AulaDto>
    {
        private readonly ICursoService _cursoService;
        private readonly IMapper _mapper;

        public CursosCommandHandler(ICursoService cursoService, IMapper mapper)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        public async Task<CursoDto> Handle(CadastrarCursoCommand request, CancellationToken cancellationToken)
        {
            var curso = new Curso(request.Nome, request.Descricao, request.Valor, new ConteudoProgramatico(request.NumeroAulas, request.MaterialDidatico));
            await _cursoService.CadastrarCursoAsync(curso);
            return _mapper.Map<CursoDto>(curso);
        }

        public async Task<AulaDto> Handle(CadastrarAulaCommand request, CancellationToken cancellationToken)
        {
            var aula = new Aula(request.cursoId, request.Titulo);
            await _cursoService.CadastrarAulaAsync(aula);
            return _mapper.Map<AulaDto>(aula);
        }
    }
}
