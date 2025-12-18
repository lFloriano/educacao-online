namespace EducacaoOnline.Alunos.AntiCorruption
{
    public class ConteudoGateway : IConteudoGateway
    {
        private readonly IConteudoApiClient _client;

        public ConteudoGateway(IConteudoApiClient client)
        {
            _client = client;
        }

        public async Task<CursoDto> ObterCursoAsync(Guid cursoId)
        {
            var dto = await _client.GetCursoAsync(cursoId);
            return new CursoDto(dto.Id, dto.Titulo, dto.TotalAulas);
        }

        public async Task<bool> AulaPertenceAoCursoAsync(Guid cursoId, Guid aulaId)
        {
            var aula = await _client.GetAulaAsync(cursoId, aulaId);
            return aula != null;
        }
    }
}
