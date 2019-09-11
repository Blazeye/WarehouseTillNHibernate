using System;
using System.Collections.Generic;
using System.Reflection;
using EventsTillTest.till;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using WarehouseTill;
using WarehouseTill.discounts;
using WarehouseTill.model;
using WarehouseTill.till;
using WarehouseTill.warehouse;

namespace EventsTillTest.discount
{
    [TestFixture]
    public class DiscountTest
    {
        [Test]
        public void TestMultipleDiscountsOnProducts()
        {
            // Prepare
            TestProduct product = new TestProduct(1234);
            TestProductCatalogus catalogus = new TestProductCatalogus();
            TestDisplay display = new TestDisplay();
            TestProgram program = new TestProgram();
            Dictionary<string, decimal> disc = new Dictionary<string, decimal>();
            object s = null;
            ItemEventArgs e = new ItemEventArgs(product);

            var till = new Till(catalogus, new CashRegister(program.StartRegister())); // SUT = Software Under Test
            var sut = new DiscountManager();
            sut.ItemDiscounted += till.HandleAddDiscount;
            disc = till.discount;


            // Run & Analyze
            sut.HandleCheckDiscount(s, e);
            sut.HandleCheckDiscount(s, e);
            Assert.IsEmpty(disc);
            sut.HandleCheckDiscount(s, e);
            Assert.IsNotNull(disc);
            Assert.AreEqual(1, disc.Count);
            Assert.AreEqual((0.75m * 1234m), disc["1234"]);
            sut.HandleCheckDiscount(s, e);
            sut.HandleCheckDiscount(s, e);
            Assert.AreEqual(1, disc.Count);
            Assert.AreEqual((0.75m * 1234m), disc["1234"]);

        }
        [Test]
        public void TestDiscountAfterBuyingProducts()
        {
            // Prepare
            TestProductCatalogus catalogus = new TestProductCatalogus();
            TestDisplay display = new TestDisplay();
            TestProgram program = new TestProgram();
            var till = new Till(catalogus, new CashRegister(program.StartRegister())); // SUT = Software Under Test
            var sut = new DiscountManager();
            sut.ItemDiscounted += till.HandleAddDiscount;
            TestProduct product = new TestProduct(1234);

            Dictionary<string, decimal> disc = new Dictionary<string, decimal>();
            object s = null;
            ItemEventArgs e = new ItemEventArgs(product);


            
            //disc = till.discount;


            // Run & Analyze
            sut.HandleCheckDiscount(s, e);
            sut.HandleCheckDiscount(s, e);
            Assert.IsEmpty(till.discount);
            sut.HandleClearDiscountCheck(s, e);
            sut.HandleCheckDiscount(s, e);
            sut.HandleCheckDiscount(s, e);
            Assert.IsEmpty(till.discount);
        }
    }
}
