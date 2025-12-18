using AutoMapper;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Application.Queries;
using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.Services;
using EducacaoOnline.Core.Communication.Dtos;
using EducacaoOnline.Core.DomainObjects;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Handlers
{
    public class AlunosQueryHandler :
        IRequestHandler<ObterMatriculasQuery, IEnumerable<MatriculaDto>>,
        IRequestHandler<ObterAlunoPorIdQuery, AlunoDto?>,
        IRequestHandler<ObterAlunoPorEmailQuery, AlunoDto?>,
        IRequestHandler<ObterAlunoResumoQuery, AlunoResumoDto?>
    {
        private readonly IAlunoService _alunoService;
        private readonly IMapper _mapper;

        public AlunosQueryHandler(IAlunoService alunoService, IMapper mapper)
        {
            _alunoService = alunoService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatriculaDto>> Handle(ObterMatriculasQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoService.ObterPorIdAsync(request.alunoId);

            if (aluno == null)
                throw new NotFoundException(nameof(Aluno), request.alunoId);

            return _mapper.Map<IEnumerable<MatriculaDto>>(aluno.Matriculas);
        }

        public async Task<AlunoDto?> Handle(ObterAlunoPorIdQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoService.ObterPorIdAsync(request.id);

            if (aluno == null)
                throw new NotFoundException(nameof(Aluno), request.id);

            return _mapper.Map<AlunoDto?>(aluno);
        }

        public async Task<AlunoDto?> Handle(ObterAlunoPorEmailQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoService.ObterPorEmailAsync(request.email);

            if (aluno == null)
                throw new NotFoundException(nameof(Aluno), request.email);

            return _mapper.Map<AlunoDto>(aluno);
        }

        public async Task<AlunoResumoDto?> Handle(ObterAlunoResumoQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoService.ObterPorIdAsync(request.CursoId);

            if (aluno == null)
                return null;

            return new AlunoResumoDto(aluno.Id, aluno.Nome, aluno.Email);
        }
    }
}
