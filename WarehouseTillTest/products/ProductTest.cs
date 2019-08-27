using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTill.products;
using WarehouseTill.till;

namespace EventsTillTest.products
{
    [TestFixture]
    public class ProductTest
    {
        [Test]
        public void TestConstructor()
        {
            //Prepare

            //Run & Analyze

            Assert.Throws<ArgumentException>(() => { new Product("00000", "aaa", 0.001m); });
            Assert.Throws<ArgumentException>(() => { new Product("0000", "aaa", 0.0001m); });
        }

    }
}
