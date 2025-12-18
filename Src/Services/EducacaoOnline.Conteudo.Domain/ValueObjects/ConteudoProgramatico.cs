using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Conteudo.Domain.ValueObjects
{
    public class ConteudoProgramatico : IEquatable<ConteudoProgramatico>
    {
        public int NumeroAulas { get; }
        public string MaterialDidatico { get; } = string.Empty;

        protected ConteudoProgramatico() { }

        public ConteudoProgramatico(int numeroAulas, string materialDidatico)
        {
            NumeroAulas = numeroAulas;
            MaterialDidatico = materialDidatico.Trim();

            Validar();
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(MaterialDidatico, "O material didático não pode ser vazio.");
            Validacoes.ValidarMinimoMaximo(NumeroAulas, 0, 100, "O número de aulas deve estar entre 0 e 100");
        }

        public bool Equals(ConteudoProgramatico? outro)
        {
            if (outro is null)
                return false;

            return
                NumeroAulas == outro.NumeroAulas
                && string.Equals(MaterialDidatico, outro.MaterialDidatico, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as ConteudoProgramatico);

        public override int GetHashCode() => HashCode.Combine(NumeroAulas, MaterialDidatico.ToLowerInvariant());

        public static bool operator ==(ConteudoProgramatico left, ConteudoProgramatico right) => Equals(left, right);

        public static bool operator !=(ConteudoProgramatico left, ConteudoProgramatico right) => !Equals(left, right);
    }

}
