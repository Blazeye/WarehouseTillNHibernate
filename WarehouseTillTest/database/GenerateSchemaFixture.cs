using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using WarehouseTill.products;
using NUnit.Framework;
using System.Reflection;
using System;



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

            cfg.Configure(path + "#/WarehouseTill/WarehouseTill/database/hibernate.cfg.xml");

            cfg.AddAssembly(typeof(Product).Assembly);

            new SchemaExport(cfg).Execute(true, true, false);
        }
    }
}
