using Moq;
using Xunit; // Xunit'i eklemeyi unutma
using Unit_Test_API;

namespace XUnit_Test.Test
{
    public class CalculatorTest
    {
        // Sınıf seviyesinde tanımlamalar
        public Calculator calculator { get; set; }
        public Mock<ICalculatorService> mymock { get; set; }

        public CalculatorTest()
        {
            // 1. Mock'u oluşturuyoruz
            mymock = new Mock<ICalculatorService>();

            // 2. Calculator'a bu mock'u enjekte ediyoruz (Dependency Injection)
            // Artık calculator nesnesi, buradaki 'mymock' ne derse onu yapacak.
            calculator = new Calculator(mymock.Object);
        }

        [Theory]
        [InlineData(5, 10, 15)]
        [InlineData(20, 30, 50)]
        public void Add_simpleValues_ReturnTotalValue(int a, int b, int expextedTotal)
        {
            // HATA BURADAYDI: Burada 'var mymock = ...' diyerek yeni bir mock oluşturma!
            // Yukarıdaki global 'mymock'u kullan.

            // Arrange
            // Service'in add metodu çağrıldığında ne döneceğini ayarlıyoruz (Setup)
            mymock.Setup(x => x.add(a, b)).Returns(expextedTotal);

            // Act
            var actualTotal = calculator.add(a, b);

            // Assert
            Assert.Equal(expextedTotal, actualTotal);

            // İstersen burada da verify yapabilirsin
            mymock.Verify(x => x.add(a, b), Times.Once);
        }

        [Theory]
        [InlineData(0, 10, 0)]
        [InlineData(10, 0, 0)]
        public void Add_zeroValues_ReturnZeroValue(int a, int b, int expextedTotal)
        {
            // Arrange
            // Mock servisin bu değerler için 0 döneceğini belirtiyoruz.
            mymock.Setup(x => x.add(a, b)).Returns(expextedTotal);

            // Act
            // Calculator içindeki add metodu çalışıyor -> O da gidip Mock Service'i çağırıyor
            var actualTotal = calculator.add(a, b);

            // Assert
            Assert.Equal(expextedTotal, actualTotal);

            // Verify
            // Calculator gerçekten servise gitti mi? Evet, şimdi bu verify geçer.
            mymock.Verify(x => x.add(a, b), Times.AtLeast(2));
        }


        [Theory]
        [InlineData(3, 5, 15)]
        public void Multip_SimpleValues_ReturnsMultipValue(int a, int b, int expextedTotal)
        {

            int actualMultip=0;
            mymock.Setup(x => x.multip(It.IsAny<int>(), It.IsAny<int>
                ())).Callback<int, int>((x, y) => actualMultip = x * y);

            calculator.multip(a, b);

            Assert.Equal(15,actualMultip );
            Assert.Equal(expextedTotal, calculator.multip(2, 5));





        }





        [Theory]
        [InlineData(5, 10)]
        public void Multip_ZeroValue_ReturnsException(int a ,int b)
        {
            mymock.Setup(x=>x.multip(a,b)).Throws(new Exception("a=0 olamaz"));

            Exception exception = Assert.Throws<Exception>(() => calculator.multip(a, b));
        
        Assert.Equal("a=0 olamaz", exception.Message);



        }






    }
}