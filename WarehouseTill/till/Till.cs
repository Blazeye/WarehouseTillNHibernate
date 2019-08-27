using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseTill.display;
using WarehouseTill.products;

namespace WarehouseTill.till
{
    public class Till : ITill
    {
        public List<IProduct> cart = new List<IProduct>();
        //private decimal sum { get; set; }
        private IProductCatalog Catalog { get; set; }
        private ICashRegister Register { get; }
        private ITillDisplay Display { get; set; }
        private SortedDictionary<decimal, int> initialContent { get; }
        //private CashRegister register { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="catalogus">The product catalogus to supply the products and
        /// search of products</param>
        public Till(IProductCatalog catalogus, ICashRegister cashRegister)
        {
            if (catalogus == null || cashRegister == null)
            {
                throw new ArgumentNullException("The catalog or register does not exist");
            }
                this.Catalog = catalogus;
                this.Register = cashRegister;
        }

        /// <summary>
        /// Handle a scan of a barcode
        /// </summary>
        /// <param name="barcode">The scanned barcode</param>
        /// <returns><c>true</c> if succesfull, <c>false</c> otherwise</returns>
        public bool HandleBarcode(string barcode)
        {

            IProduct product = Catalog.FindProductForBarcode(barcode);
            if (product != null)
            {
                cart.Add(product);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Initiate a payment
        /// </summary>
        /// <param name="amount">The amount paid</param>
        /// <returns>the change to return as a list of (coinvalue => quantity) 
        ///          or <code>null</code> on failure</returns>
        public IDictionary<decimal, int> InitiatePayment(decimal amount)
        {
            decimal sum = CalculateAmountOfItems();
            if (this.cart.Count == 0)
            {
                Console.WriteLine("Shopping cart is empty...");
                return null;
            }
            var result = Register.MakeChange(sum, amount);
            if (result == null)
            {
                return result;
            }
            cart.Clear();
            return result;
        }

        /// <summary>
        /// Installs an interface to be used when displaying
        /// </summary>
        /// <param name="tillDisplay">The interface to use from now on</param>
        public void SetDisplayInterface(ITillDisplay tillDisplay)
        {
            if(tillDisplay == null)
            {

                throw new NullReferenceException("Display is null");

            }
            this.Display = tillDisplay;
        }

        /// <summary>
        /// Trigger a show all products
        /// </summary>
        public void ShowAllProducts()
        {
            string title = "PRODUCTEN:";
            try
            {
                IList<IProduct> items = this.Catalog.GetAllProducts();
                this.Display.DisplayProducts(title, items);
            }
            catch (ArgumentNullException e1)
            {
                Console.WriteLine(e1.ToString());
                throw;
            }
        }

        /// <summary>
        /// Trigger a show all scanned items
        /// </summary>
        public void ShowScannedItems()
        {
            if (cart.Count > 0)
            {
                int productIndex = cart.Count - 1;
                decimal sum = CalculateAmountOfItems();
                this.Display.DisplayClientScreen("TOTAAL: \u20ac " + Convert.ToString(Decimal.Round(sum,2)), cart[productIndex].Description);
            }
        }

        public decimal CalculateAmountOfItems()
        {
            decimal sum = Decimal.Round(cart.Sum(p => p.Amount), 2);
            return sum;
        }
    }
}
