
using NUnit.Framework;
using System;
using NHibernate;
using NHibernate.Cfg;
using WarehouseTill.model;
using WarehouseTill.repository;
using System.Reflection;


namespace WarehouseTillTest.database
{
    [TestFixture]
    public class OrdersProductRepository_Fixture

    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        [OneTimeSetUp]
        public void TestFixtureSetUp() // done for each 'run'

        {
            _configuration = new Configuration();
            //GetExecutingAssembly gets the part of the assembly that contains the .NET code that will
            //will be executing, so not the metadata.
            //CodeBase gets the location of that part of the executing assembly's code.
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            //creates an object representation of a uri based on the location
            //uri 
            UriBuilder uri = new UriBuilder(codeBase);
            //returns the uri path as string without the escape sequences
            //path without unescape: file%3A///localhost/c%24/Windows/foo.txt
            //path with unescape example: file:///localhost/c$/Windows/foo.txt
            string path = Uri.UnescapeDataString(uri.Path);

            _configuration.Configure(path + "#/WarehouseTill/WarehouseTill/database/hibernate.cfg.xml");

            _configuration.AddAssembly(typeof(OrdersProduct).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }
        [SetUp]
        public void TestSetUp()
        { // done for every test
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.CreateQuery("delete from OrdersProduct where Price=8.1818m").ExecuteUpdate();
            }
        }
        [Test]
        public void TestCanAddNewOrder()
        {
            // prepare
            var ordersProduct = new OrdersProduct(14, 8.1818m) { Item_id=3, Order_id=29 };
            IOrdersProductRepository repository = new OrdersProductRepository();

            // run
            repository.Add(ordersProduct);

            // validate
            // use session to try to load the product

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<OrdersProduct>(ordersProduct.Id);

                // Test that the product was successfully inserted

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(ordersProduct, fromDb);
                Assert.AreEqual(ordersProduct.Id, fromDb.Id);
                Assert.AreEqual(ordersProduct.Amount, fromDb.Amount);
            }
        }
        public void TestGetById()
        {
            // prepare
            OrdersProduct ordersProduct = new OrdersProduct(1, 8.1818m) { Item_id = 3, Order_id = 29 };
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(ordersProduct);
            }
            IPurchaseRepository repository = new PurchaseRepository();

            // run
            var result = repository.GetById(ordersProduct.Id);

            // validate
            Assert.NotNull(result);
            Assert.AreEqual(ordersProduct, result);

        }
        [Test]
        public void TestCanUpdate()
        {
            //Prepare
            OrdersProduct originalOrdersProduct = new OrdersProduct(2, 10m) { Item_id = 3, Order_id = 29 };
            OrdersProduct testOrdersProduct = new OrdersProduct(3, 8.1818m) { Item_id = 3, Order_id = 29 };
            OrdersProductRepository repository = new OrdersProductRepository();

            //Run
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(originalOrdersProduct);
                testOrdersProduct.Id = originalOrdersProduct.Id;
                repository.Update(testOrdersProduct);
            }
            //Validate
            using (ISession session = _sessionFactory.OpenSession())
            {
                //second session is used so fromDB get to take the changed table entry
                var fromDb = session.Get<OrdersProduct>(originalOrdersProduct.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreEqual(testOrdersProduct.Amount, fromDb.Amount);
            }
        }
        [Test]
        public void TestCanDelete()
        {
            //prepare
            OrdersProduct testOrdersProduct = new OrdersProduct(4, 8.1818m) { Item_id = 3, Order_id = 29 };
            OrdersProductRepository repository = new OrdersProductRepository();

            //run
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(testOrdersProduct);
                repository.Remove(testOrdersProduct);
            }
            //validate
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Purchase>(testOrdersProduct.Id);
                Assert.IsNull(fromDb);
            }
        }
    }
}

