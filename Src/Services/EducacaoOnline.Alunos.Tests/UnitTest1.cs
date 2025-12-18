using System.Linq.Expressions;

namespace EducacaoOnline.Alunos.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            var teste = true;

            //act
            teste = true;

            //assert
            Assert.True(teste);
        }
    }
}
