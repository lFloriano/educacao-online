using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Conteudo.Domain
{
    public class Aula : Entity
    {
        public Aula(Guid cursoId, string titulo)
        {
            CursoId = cursoId;
            Titulo = titulo;
            DataCadastro = DateTime.Now;

            Validar();
        }

        public Guid CursoId { get; private set; }
        public string Titulo { get; private set; } = string.Empty;
        public DateTime DataCadastro { get; private set; }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Titulo, "Tilulo da aula não pode ser vazio");
        }
    }
}

