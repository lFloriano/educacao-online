using EducacaoOnline.Conteudo.Data;
using EducacaoOnline.Conteudo.Domain;
using EducacaoOnline.Conteudo.Domain.ValueObjects;

namespace EducacaoOnline.Api.Configurations.Seeder;

public static class SeederConteudo
{
    public static void Seed(CursosDbContext cursosDbContext)
    {
        if (!cursosDbContext.Cursos.Any())
        {
            cursosDbContext.Add(GerarCursoComAulas());
            cursosDbContext.Cursos.AddRange(GerarCursosSemAulas());
            cursosDbContext.SaveChanges();
        }
    }

    private static Curso GerarCursoComAulas()
    {
        var curso = new Curso(
            "Curso Avançado de Calistenia",
            "Aprenda progressões, técnicas de força e mobilidade para exercícios avançados de calistenia.",
            250M,
            new ConteudoProgramatico(4, "Vídeos detalhados, planilha de treino e material sobre nutrição")
        );

        curso.CadastrarAula(new Aula(curso.Id, "Treino de Força para Handstand"));
        curso.CadastrarAula(new Aula(curso.Id, "Progressão para Front Lever"));
        curso.CadastrarAula(new Aula(curso.Id, "Como melhorar a mobilidade de ombros"));
        curso.CadastrarAula(new Aula(curso.Id, "Treino de Core e Controle Corporal"));
        return curso;
    }

    private static Curso[] GerarCursosSemAulas()
    {
        return new[]
        {
            new Curso(
                "Curso de C# para Iniciantes",
                "Aprenda os fundamentos da linguagem C# e orientação a objetos.",
                150M,
                new ConteudoProgramatico(20, "Apostila completa em PDF + exercícios práticos")
            ),
            new Curso(
                "ASP.NET Core Avançado",
                "Desenvolvimento de APIs e aplicações web modernas com ASP.NET Core 9.",
                160M,
                new ConteudoProgramatico(35, "Slides, exemplos de código e projetos guiados")
            ),
            new Curso(
                "Entity Framework Core na Prática",
                "Mapeamento objeto-relacional e boas práticas com EF Core.",
                180M,
                new ConteudoProgramatico(25, "Scripts SQL e guia de performance")
            ),
            new Curso(
                "Introdução à Calistenia",
                "Curso básico para iniciantes em calistenia, com foco em técnica e progressão.",
                190M,
                new ConteudoProgramatico(15, "Vídeos explicativos e plano semanal de treino")
            ),
        };
    }
}

