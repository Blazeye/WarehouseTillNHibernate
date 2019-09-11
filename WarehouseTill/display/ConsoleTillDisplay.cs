using System;
using System.Collections.Generic;
using System.Text;
using WarehouseTill.model;
using WarehouseTill.products;
using WarehouseTill.till;

namespace WarehouseTill.display
{
    /// <summary>
    /// Class for all interaction with the console for the till
    /// </summary>
    class ConsoleTillDisplay
    {
        private ITill Till { get; }

        public event EventHandler ShowingScanned;
        public event EventHandler ShowingProducts;

        public ConsoleTillDisplay(ITill till)
        {
            if (till == null)
            {
                throw new till.ArgumentNullException("object is null");
            }
            try
            {
                Till = till;
                // Install ourselfs as display target
                //Till.SetDisplayInterface((ITillDisplay)this);
            }
            catch (till.ArgumentNullException e2)
            {
                Console.WriteLine(e2.ToString());
            }
        }

        private void RaiseShowingScanned()
        {
            EventHandler handler = ShowingScanned;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void RaiseShowingProducts()
        {
            EventHandler handler = ShowingProducts;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            RaiseShowingProducts();
            ShowMenu();
            string input = Console.ReadLine().ToLower().Trim();

            while (input != "q")
            {
                switch (input.Substring(0, 1))
                {
                    case "p":
                        RaiseShowingProducts();
                        break;

                    case "b":
                        if (input == "b")
                        {
                            Console.WriteLine("Customer hasn't paid\n");
                            break;
                        }
                        decimal amount = decimal.Parse(input.Substring(1));
                        var result = Till.InitiatePayment(amount);
                        if (result == null)
                        {
                            Console.Out.WriteLine("Payment failed\n");
                        }
                        else if (result.Count == 0)
                        {
                            var order = new Purchase();

                            Till.AddOrder();
                            Console.Out.WriteLine("Payment successful\n");
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
                            Till.AddOrder();
                            Console.Out.WriteLine("Payment done, please return {0}\n", resultString.ToString());
                        }
                        break;

                    default:
                        if (!Till.HandleBarcode(input))
                        {
                            Console.Out.WriteLine("Product could not be added\n");
                        }
                        break;
                }
                ShowMenu();
                RaiseShowingScanned();
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
    }
}
