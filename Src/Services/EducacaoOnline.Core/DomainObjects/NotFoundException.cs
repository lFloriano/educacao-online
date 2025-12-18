namespace EducacaoOnline.Core.DomainObjects
{
    public class NotFoundException : Exception
    {
        public string NomeRecurso { get; }
        public object Chave { get; }

        public NotFoundException(string nomeRecurso, object chave) : base($"O recurso '{nomeRecurso}' com a chave '{chave}' não foi encontrado.")
        {
            NomeRecurso = nomeRecurso;
            Chave = chave;
        }
    }
}
