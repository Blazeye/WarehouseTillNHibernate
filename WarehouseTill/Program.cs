using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

using System;
using System.Linq;
using System.Reflection;
using WarehouseTill.display;
using WarehouseTill.products;
using WarehouseTill.till;
using System.Collections.Generic;
using System.Threading;

namespace WarehouseTill
{
    public class Program
    {
        static void Main(string[] args)
        {

//            var cfg = new Configuration();

//            String DataSource = (localdb)\MSSQLLocalDB;
//            String InitialCatalog = WareHouse;
//            String IntegratedSecurity = True;
//            String ConnectTimeout = 30;
//            String Encrypt = "False";
//            String TrustServerCertificate = "False";
//            String ApplicationIntent = "ReadWrite;
//            String MultiSubnetFailover = False;

//            cfg.DataBaseIntegration(x => {
//                x.ConnectionString = "Data Source + " +
//"Initial Catalog + Integrated Security + Connect Timeout + " +
//"Encrypt + TrustServerCertificate + ApplicationIntent + " +
//"MultiSubnetFailover";

//                x.Driver<SqlClientDriver>();
//                x.Dialect<MsSql2008Dialect>();
//            });

//            cfg.AddAssembly(Assembly.GetExecutingAssembly());
//            var sefact = cfg.BuildSessionFactory();

//            using (var session = sefact.OpenSession())
//            {
//                using (var tx = session.BeginTransaction())
//                {
//                    //perform database logic
//                    tx.Commit();
//                }

//                Console.ReadLine();
//            }




            SortedDictionary<decimal, int> StartRegister()
                {
                SortedDictionary<decimal, int> initialContent = new SortedDictionary<decimal, int>(new ReversedComparer()) {

                      {   50m,  0 }, // no bills of 50 euro
                      {   20m,  1 }, //  1 bill  of 20 euro
                      {   10m,  0 }, // no bills of 10 euro
                      {    5m,  1 }, //  1 bill  of  5 euro
                      {    2m,  1 }, //  1 coin  of  2 euro
                      {    1m,  4 }, //  4 coins of  1 euro
                      {  0.5m,  2 }, //  2 coins of 50 cent
                      {  0.2m, 10 }, // 10 coins of 20 cent
                      {  0.1m,  3 }, //  3 coins of 10 cent
                      { 0.05m,  7 }, //  7 coins of  5 cent
                      { 0.02m,  1 }, //  1 coin  of  2 cent
                      { 0.01m,  4 }, //  4 coins of  1 cent

                };

                return initialContent;
            }


            //ICashRegister register = new CashRegister(StartRegister());

            // Make a new 'till' based on the ITill interface
            ITill till = new Till(new ProductCatalog(), new CashRegister(StartRegister()));

            // Construct a console interaction file
            var consoleInteraction = new ConsoleTillDisplay(till);

            // Start the main loop
            consoleInteraction.Run();


        }
    }
}
