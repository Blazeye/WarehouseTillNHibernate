using NUnit.Framework;

namespace WarehouseTill.products
{
    [TestFixture]
    public class ProductCatalogTest
    {
        [Test]
        public void testGetAllProducts()
        {
            // Prepare
            var sut = new ProductCatalog();

            // Run
            var result = sut.GetAllProducts();

            // Analyze
            //Must do the test at least one to add the test product
            Assert.NotNull(result);
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("1234", result[0].Barcode);
        }

        // TODO add more tests
    }
}
