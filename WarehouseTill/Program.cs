using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

using WarehouseTill.display;
using WarehouseTill.products;
using WarehouseTill.till;
using WarehouseTill.warehouse;
using WarehouseTill.printer;
using WarehouseTill.discounts;
using System.Collections.Generic;

namespace WarehouseTill
{
    public class Program
    {
        static void Main(string[] args)
        {


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


            // Make a new 'till' based on the ITill interface
            Till till = new Till(new ProductCatalog(), new CashRegister(StartRegister()));
            Warehouse wh = new Warehouse();
            Printer pr = new Printer();
            DiscountManager dsc = new DiscountManager();

            till.ItemScanned += wh.HandleAddItem;
            till.ItemScanned += till.HandleFilledCart;
            till.ItemScanned += dsc.HandleCheckDiscount;
            dsc.ItemDiscounted += till.HandleAddDiscount;
            till.ClearDiscountCheck += dsc.HandleClearDiscountCheck;
            till.ItemPayed += pr.HandlePrintItems;
            

            // Construct a console interaction file
            var consoleInteraction = new ConsoleTillDisplay(till);
            consoleInteraction.ShowingScanned += till.HandleShowingScanned;
            consoleInteraction.ShowingProducts += till.HandleShowingProducts;

            // Start the main loop
            consoleInteraction.Run();

        }
    }
}
