using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using WarehouseTill.products;
using NUnit.Framework;



namespace WarehouseTillTest.database

{
    [TestFixture]
    public class GenerateSchema_Fixture
    {
        [Test, Ignore("This clears the entire database")] // Do not run automaticly, only run when triggerd manually
        public void Can_generate_schema()
        {
            //            System.Data.SqlClient.db c = null;

            var cfg = new Configuration();
            cfg.Configure("C:/Users/StJac/source/repos/WarehouseTill2/WarehouseTill_C#/WarehouseTill/WarehouseTill/database/hibernate.cfg.xml");
            cfg.AddAssembly(typeof(Product).Assembly);

            new SchemaExport(cfg).Execute(true, true, false);
        }
    }
}
