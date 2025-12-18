using EducacaoOnline.Conteudo.Domain.ValueObjects;
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Conteudo.Domain
{
    public class Curso : Entity, IAggregateRoot
    {
        private readonly List<Aula> _aulas = new();
        public IReadOnlyCollection<Aula> Aulas => _aulas;

        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; private set; }

        public ConteudoProgramatico ConteudoProgramatico { get; set; }

        protected Curso() { }

        public Curso(string nome, string descricao, decimal valor, ConteudoProgramatico conteudoProgramatico)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            ConteudoProgramatico = conteudoProgramatico;

            Validar();
        }

        public Aula CadastrarAula(Aula aula)
        {
            ValidarSeAulaJaExisteNoCurso(aula);
            ValidarSeExisteAulaComMesmoTitulo(aula);
            ValidarNumeroMaximoDeAulas();

            _aulas.Add(aula);
            return aula;
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O nome do curso não pode ser vazio");
            Validacoes.ValidarSeVazio(Nome, "A descrição do curso não pode ser vazia");
            Validacoes.ValidarSeMenorQue(Valor, 0, "O valor do curso deve ser maior que zero");
        }

        private void ValidarSeAulaJaExisteNoCurso(Aula aula)
        {
            if (Aulas.Any(x => x.Id == aula.Id))
                throw new DomainException("Aula já existe no curso");
        }

        private void ValidarSeExisteAulaComMesmoTitulo(Aula aula)
        {
            if (Aulas.Any(x => x.Titulo.ToLower() == aula.Titulo.ToLower()))
                throw new DomainException("Já existe aula com mesmo título neste curso");
        }

        private void ValidarNumeroMaximoDeAulas()
        {
            if (Aulas.Count() == ConteudoProgramatico.NumeroAulas)
                throw new DomainException("Número máximo de aulas atingido");
        }
    }
}
