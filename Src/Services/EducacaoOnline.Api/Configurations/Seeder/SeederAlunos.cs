using EducacaoOnline.Alunos.Data;
using EducacaoOnline.Alunos.Domain;

namespace EducacaoOnline.Api.Configurations.Seeder;

public static class SeederAlunos
{
    public static void Seed(AlunosDbContext alunosDbContext)
    {
        GerarSeed(alunosDbContext);
    }

    private static void GerarSeed(AlunosDbContext alunosDbContext)
    {
        if (!alunosDbContext.Alunos.Any())
        {
            alunosDbContext.Alunos.AddRange(
                new[]
                {
                    new Aluno(Guid.NewGuid(),"João da Silva", "joao@teste.com"),
                    new Aluno(Guid.NewGuid(), "Maria Souza", "maria@teste.com" ),
                    new Aluno(Guid.NewGuid(), "Fulano Marciano", "fulano@teste.com" )
                }
            );

            alunosDbContext.SaveChanges();
        }
    }
}

