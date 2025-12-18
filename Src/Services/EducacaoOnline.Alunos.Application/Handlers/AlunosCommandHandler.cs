using AutoMapper;
using EducacaoOnline.Alunos.Application.Commands;
using EducacaoOnline.Alunos.Application.Dtos;
using EducacaoOnline.Alunos.Domain;
using EducacaoOnline.Alunos.Domain.Enums;
using EducacaoOnline.Alunos.Domain.Services;
using EducacaoOnline.Core.Communication.Gateways;
using EducacaoOnline.Core.DomainObjects;
using MediatR;

namespace EducacaoOnline.Alunos.Application.Handlers
{
    public class AlunosCommandHandler :
        IRequestHandler<CadastrarAlunoCommand, Guid>,
        IRequestHandler<MatricularAlunoCommand, MatriculaCriadaDto>,
        IRequestHandler<AtivarMatriculaCommand, SituacaoMatricula>,
        IRequestHandler<RealizarAulaCommand, AulaConcluidaDto>,
        IRequestHandler<FinalizarCursoCommand, SituacaoMatricula>
    {
        private readonly IAlunoService _alunoService;
        private readonly IMapper _mapper;
        private readonly IConteudoGateway _conteudoGateway;

        public AlunosCommandHandler(IAlunoService alunoService, IMapper mapper, IConteudoGateway conteudoGateway)
        {
            _alunoService = alunoService;
            _mapper = mapper;
            _conteudoGateway = conteudoGateway;
        }

        public async Task<MatriculaCriadaDto> Handle(MatricularAlunoCommand request, CancellationToken cancellationToken)
        {
            var curso = await _conteudoGateway.ObterCursoAsync(request.CursoId);

            if (curso == null)
                throw new NotFoundException("Curso", request.CursoId);

            var matricula = await _alunoService.MatricularAsync(request.AlunoId, request.CursoId);
            return _mapper.Map<MatriculaCriadaDto>(matricula);
        }

        public async Task<AulaConcluidaDto> Handle(RealizarAulaCommand request, CancellationToken cancellationToken)
        {
            var curso = await _conteudoGateway.ObterCursoAsync(request.CursoId)
                ?? throw new InvalidOperationException("Curso não encontrado no catálogo de Conteúdo.");

            var aulaPertenceAoCurso = await _conteudoGateway.AulaPertenceAoCursoAsync(request.CursoId, request.AulaId);

            if (!aulaPertenceAoCurso)
                throw new InvalidOperationException("Aula não encontrada no catálogo do Curso.");

            var aulaConcluida = await _alunoService.RealizarAulaAsync(request.AlunoId, request.CursoId, request.AulaId);
            return _mapper.Map<AulaConcluidaDto>(aulaConcluida);
        }

        public async Task<SituacaoMatricula> Handle(FinalizarCursoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoService.ObterPorIdAsync(request.AlunoId)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            var matricula = aluno.ObterMatriculaAtivaPorCursoId(request.CursoId)
                ?? throw new InvalidOperationException("Aluno não possui matrícula ativa para o curso.");

            var curso = await _conteudoGateway.ObterCursoAsync(request.CursoId)
                ?? throw new InvalidOperationException("Curso não encontrado no catálogo de Conteúdo.");

            var aulasConcluidas = matricula.AulasConcluidas.Count;

            if (aulasConcluidas < curso.NumeroAulas)
                throw new InvalidOperationException("Aluno não concluiu todas as aulas do curso.");

            var matriculaFinalizada = await _alunoService.FinalizarCursoAsync(request.AlunoId, request.CursoId);
            return matricula.Situacao;
        }

        public async Task<Guid> Handle(CadastrarAlunoCommand request, CancellationToken cancellationToken)
        {
            //TODO: validar nome e email
            //TODO: isto deveria ser um cadastro de usuário em outro BC???

            var aluno = new Aluno(Guid.NewGuid(), request.Nome, request.Email);
            return await _alunoService.CadastrarAlunoAsync(aluno);
        }

        public async Task<SituacaoMatricula> Handle(AtivarMatriculaCommand request, CancellationToken cancellationToken)
        {
            var curso = await _conteudoGateway.ObterCursoAsync(request.CursoId)
                ?? throw new NotFoundException("Curso", request.CursoId);

            return (await _alunoService.AtivarMatriculaAsync(request.AlunoId, request.CursoId)).Situacao;
        }
    }
}
