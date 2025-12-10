using Microsoft.AspNetCore.Mvc; // OkObjectResult için gerekli
using Moq;
using Unit_Test_API;             // Product ve IRepository'nin olduğu yer
using Unit_Test_API.Controllers; // ProductController'ın olduğu yer

namespace XUnit_Test.Test
{
    public class ProductControllerTest
    {
        // 1. Mock ve Controller Tanımları
        private readonly Mock<IRepository<Product>> _mockRepo;
        private readonly ProductController _controller;
        private List<Product> _products;

        public ProductControllerTest()
        {
            // 2. Mock Nesnesini Oluşturma
            _mockRepo = new Mock<IRepository<Product>>();

            // 3. Controller'ı Mock ile Başlatma (Dependency Injection)
            _controller = new ProductController(_mockRepo.Object);

            // 4. Test Verilerini Hazırlama (Stock ve Color eklendi)
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Kalem", Price = 100, Stock = 50, Color = "Kırmızı" },
                new Product { Id = 2, Name = "Defter", Price = 200, Stock = 100, Color = "Mavi" },
                new Product { Id = 3, Name = "Silgi", Price = 300, Stock = 20, Color = "Yeşil" }
            };
        }

        // ÖRNEK TEST: Tüm ürünleri getirme testi
        [Fact]
        public void GetAll_ActionExecutes_ReturnOkResultWithProducts()
        {
            // Arrange (Hazırlık)
            // Repo.GetAll() çağrıldığında yukarıdaki listeyi dönsün.
            _mockRepo.Setup(x => x.GetAll()).Returns(_products);

            // Act (Eylem)
            var result = _controller.GetAll();

            // Assert (Doğrulama)
            // 1. Dönüş tipi OkObjectResult (200 OK) mu?
            var okResult = Assert.IsType<OkObjectResult>(result);

            // 2. Dönüş değerinin içindeki veri List<Product> tipinde mi?
            var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            // 3. Gelen listede 3 tane mi ürün var?
            Assert.Equal(3, returnProducts.Count());
        }
    }
}