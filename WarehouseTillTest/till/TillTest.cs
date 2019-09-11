using System;
using System.Collections.Generic;
using EventsTillTest.till;
using NUnit.Framework;
using WarehouseTill.products;
using WarehouseTill.till;
using WarehouseTill.warehouse;
using WarehouseTill.discounts;

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

            Assert.Throws<System.ArgumentNullException>(() => new Till(null, null)); // We expect that it throws an exception
        }

        [Test]
        public void TestDiscountProducts()
        {
            // Prepare
            TestProduct product = new TestProduct(1234);
            TestProductCatalogus catalogus = new TestProductCatalogus();
            TestDisplay display = new TestDisplay();
            TestProgram program = new TestProgram();
            Dictionary<string, decimal> disc = new Dictionary<string, decimal>();
            object s = null;
            DiscountEventArgs e1 = new DiscountEventArgs(null, 0, 0);
            DiscountEventArgs e2 = new DiscountEventArgs(product, 3, .25m);

            var sut = new Till(catalogus, new CashRegister(program.StartRegister())); // SUT = Software Under Test
            disc = sut.discount;

            // Run & Analyze
            sut.HandleAddDiscount(s, e1);
            Assert.IsEmpty(disc);
            sut.HandleAddDiscount(s, e2);
            Assert.IsNotNull(disc);
            Assert.AreEqual(1, disc.Count);
            Assert.AreEqual((0.75m*1234m), disc["1234"]);

            // TODO add more tests here
        }
    }
}
