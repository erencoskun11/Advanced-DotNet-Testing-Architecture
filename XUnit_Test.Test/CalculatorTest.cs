using Unit_Test_API;

namespace XUnit_Test.Test
{
    public class CalculatorTest
    {
        [Fact]
        public void AddTest()
        {
            //Arrange

            int x = 5;
            int y = 20;

            var calculator = new Calculator();



            //Act
            var total = calculator.add(x, y);


            //Assert
            
            Assert.Equal<int>(25, total);



        }
    }
}
