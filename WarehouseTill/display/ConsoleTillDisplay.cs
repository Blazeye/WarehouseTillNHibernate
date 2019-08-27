using System;
using System.Collections.Generic;
using System.Text;
using WarehouseTill.products;
using WarehouseTill.till;

namespace WarehouseTill.display
{
    /// <summary>
    /// Class for all interaction with the console for the till
    /// </summary>
    class ConsoleTillDisplay : ITillDisplay
    {
        private ITill Till { get; }

        public ConsoleTillDisplay(ITill till)
        {
            if(till == null)
            {
                throw new till.ArgumentNullException("object is null");
            }
            try
            {
                Till = till;
                // Install ourselfs as display target
                Till.SetDisplayInterface((ITillDisplay)this);
            }
            catch(till.ArgumentNullException e2)
            {
                Console.WriteLine(e2.ToString());
            }
        }

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Till.ShowAllProducts();
            ShowMenu();
            string input = Console.ReadLine().ToLower().Trim();

            while (input != "q")
            {
                switch (input.Substring(0, 1))
                {
                    case "p":
                        Till.ShowAllProducts();
                        break;

                    case "b":
                        if(input == "b")
                        {
                            Console.WriteLine("Customer hasn't paid");
                            break;
                        }
                        decimal amount = decimal.Parse(input.Substring(1));
                        var result = Till.InitiatePayment(amount);
                        if (result == null)
                        {
                            Console.Out.WriteLine("Payment failed");
                        }
                        else if (result.Count == 0)
                        {
                            Console.Out.WriteLine("Payment successful");
                        }
                        else
                        {
                            StringBuilder resultString = new StringBuilder();
                            foreach (var pair in result)
                            {
                                if (resultString.Length != 0)
                                {
                                    resultString.Append(", ");
                                }
                                resultString.AppendFormat("{0:c}: {1}x", pair.Key, pair.Value);
                            }
                            Console.Out.WriteLine("Payment done, please return {0}", resultString.ToString());
                        }
                        break;

                    default:
                        if (!Till.HandleBarcode(input))
                        {
                            Console.Out.WriteLine("Product could not be added");
                        }
                        break;
                }
                ShowMenu();
                Till.ShowScannedItems();
                input = Console.ReadLine().ToLower().Trim();
            }
        }

        /// <summary>
        /// Show the menu
        /// </summary>
        private void ShowMenu()
        {
            Console.Out.WriteLine("P         : Toon alle producten");
            Console.Out.WriteLine("<Barcode> : Scan de barcode code");
            Console.Out.WriteLine("B<bedrag> : Doe een betaling");
            Console.Out.WriteLine("Q         : Programma afbreken");
            Console.Out.Write("> ");
        }

        /// <summary>
        /// Implements ITillDisplay.DisplayClientScreen
        /// </summary>
        public void DisplayClientScreen(string line1, string line2)
        {
            Console.Out.WriteLine("==========================================");
            Console.Out.WriteLine("|{0,-40}|", line1);
            Console.Out.WriteLine("|{0,-40}|", line2);
            Console.Out.WriteLine("==========================================");
        }

        /// <summary>
        /// Implements ITillDisplay.DisplayProducts
        /// </summary>
        public void DisplayProducts(string title, IList<IProduct> products)
        {
            Console.Out.WriteLine("======================== " + title + " =======================");
            foreach (IProduct product in products)
            {
                Console.Out.WriteLine(String.Format("{0,4} {1,-40} {2:c}",
                    product.Barcode, product.Description, product.Amount));
            }
            Console.Out.WriteLine("=========================================================");
        }
    }
}
