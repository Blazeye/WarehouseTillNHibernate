using NHibernate;
using NHibernate.Cfg;
using WarehouseTill.model;
using WarehouseTill.repository;
using NUnit.Framework;
using System.Reflection;
using System;

using WarehouseTill.database;
using System.Collections.Generic;

namespace WarehouseTillTest.database
{
    [TestFixture]
    public class OrdersProductRepositoryFixture

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

            _configuration.AddAssembly(typeof(Purchase).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }
        [SetUp]
        public void TestSetUp()
        { // done for every test
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.CreateQuery("delete from Purchase where Date='testPurchase'").ExecuteUpdate();
            }
        }
        [Test]
        public void TestCanAddNewOrder()
        {
            // prepare
            var order = new Purchase() { Date = "testPurchase" };
            IPurchaseRepository repository = new PurchaseRepository();

            // run
            repository.Add(order);

            // validate
            // use session to try to load the product

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Purchase>(order.Id);

                // Test that the product was successfully inserted

                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(order, fromDb);
                Assert.AreEqual(order.Id, fromDb.Id);
                Assert.AreEqual(order.Date, fromDb.Date);
            }
        }
        [Test]
        public void TestGetById()
        {
            // prepare
            Purchase testPurchase = new Purchase() { Date = "testPurchase" };
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(testPurchase);
            }
            IPurchaseRepository repository = new PurchaseRepository();

            // run
            var result = repository.GetById(testPurchase.Id);

            // validate
            Assert.NotNull(result);
            Assert.AreEqual(testPurchase, result);

        }
        [Test]
        public void TestCanUpdate()
        {
            //Prepare
            Purchase originalPurchase = new Purchase(){ Date = "failure" };
            Purchase testPurchase = new Purchase() { Date = "testPurchase" };
            IPurchaseRepository repository = new PurchaseRepository();

            //Run
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(originalPurchase);
                testPurchase.Id = originalPurchase.Id;
                repository.Update(testPurchase);
            }
            //Validate
            using (ISession session = _sessionFactory.OpenSession())
            {
                //second session is used so fromDB get to take the changed table entry
                var fromDb = session.Get<Purchase>(originalPurchase.Id);

                Assert.IsNotNull(fromDb);
                Assert.AreEqual(testPurchase.Date, fromDb.Date);
            }
        }
        [Test]
        public void TestCanDelete()
        {
            //prepare
            Purchase testPurchase = new Purchase() { Date="testPurchase" };
            PurchaseRepository repository = new PurchaseRepository();

            //run
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(testPurchase);
                repository.Remove(testPurchase);
            }
            //validate
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Purchase>(testPurchase.Id);
                Assert.IsNull(fromDb);
            }
        }
    }
}

