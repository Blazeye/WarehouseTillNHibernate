using EventsTillTest.till;
using NUnit.Framework;
using WarehouseTill.till;

namespace WarehouseTill
{
    [TestFixture]


    public class TillTest
    {
        [Test]
        public void TestConstructor()
        {
            // Prepare

            // Run
            TestProgram program = new TestProgram();
            var sut = new Till(new TestProductCatalogus(), new CashRegister(program.StartRegister())); // SUT = Software Under Test

            // Analyze
            Assert.NotNull(sut); // We expect that 'SUT' is a valid object
            Assert.IsInstanceOf<ITill>(sut); // We expect that it implements the ITill interface
        }

        [Test]
        public void TestEmptyConstructor()
        {
            // Prepare

            // Run & Analyze
            //TestProgram program = new TestProgram();
            //Till sut = new Till(null, null);
            //Assert.IsNull(new Till(null, null));

            Assert.Throws<ArgumentNullException>(() => new Till(null, null)); // We expect that it throws an exception
        }

        [Test]
        public void TestShowAllProducts()
        {
            // Prepare
            TestProductCatalogus catalogus = new TestProductCatalogus();
            TestDisplay display = new TestDisplay();
            TestProgram program = new TestProgram();

            var sut = new Till(catalogus, new CashRegister(program.StartRegister())); // SUT = Software Under Test
            sut.SetDisplayInterface(display);

            // Run
            sut.ShowAllProducts();

            // Analyze
            Assert.NotNull(display.ReceivedProducts); // The function ITillDisplay.ShowProducts is called
            Assert.AreEqual(2, display.ReceivedProducts.Count); // The function is supplied with 2 products
            Assert.AreEqual(catalogus.ProductList, display.ReceivedProducts); // It should be the 2 products from the catalogus
        }

        // TODO add more tests here
    }
}
