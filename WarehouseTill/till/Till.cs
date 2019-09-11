using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseTill.model;
using WarehouseTill.products;
using WarehouseTill.repository;
using WarehouseTill.warehouse;
using WarehouseTill.printer;
using WarehouseTill.discounts;

namespace WarehouseTill.till
{
    public class Till : ITill
    {
        public event EventHandler<ItemEventArgs> ItemScanned;
        public event EventHandler<OrdersListEventArgs> ItemPayed;
        public event EventHandler ClearDiscountCheck;
        public List<IProduct> cart { get; set; } = new List<IProduct>();
        public Dictionary<string, decimal> discount { get; set; } = new Dictionary<string, decimal>();
        private IProductCatalog Catalog { get; set; }
        private ICashRegister Register { get; }
        private SortedDictionary<decimal, int> initialContent { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="catalogus">The product catalogus to supply the products and
        /// search of products</param>
        public Till(IProductCatalog catalogus, ICashRegister cashRegister)
        {
            if (catalogus == null || cashRegister == null)
            {
                throw new System.ArgumentNullException("The catalog or register does not exist");
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
                RaiseItemScanned(product);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Checks the eventdata and adds a discount based on the item in it
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e">Item elligible for a discount</param>
        public void HandleAddDiscount(object s, DiscountEventArgs e)
        {
            if(e.Item == null)
            {
                return;
            }
            if (!discount.ContainsKey(e.Item.Barcode))
            {
                discount.Add(e.Item.Barcode, 0);
            }
            discount[e.Item.Barcode] += e.Number * e.Item.Amount * e.Percentage;
        }
        
        /// <summary>
        /// returns the total discount as decimal based on the discount Dictionary
        /// </summary>
        /// <returns></returns>
        private decimal CalculateTotalDiscount()
        {
            decimal totalDiscount = 0;
            if(discount == null)
            {
                return 0;
            }
            foreach(string barcode in discount.Keys)
            {
                totalDiscount += discount[barcode];
            }
            return totalDiscount;
        }

        /// <summary>
        /// Triggers event when an item is scanned
        /// </summary>
        /// <param name="product">item which was scanned</param>
        private void RaiseItemScanned(IProduct product)
        {
            EventHandler<ItemEventArgs> handler = ItemScanned;
            if(handler != null)
            {
                handler(this, new ItemEventArgs(product));
            }
        }

        /// <summary>
        /// Triggers an event when an item gets payed
        /// </summary>
        /// <param name="dict">dictionary of ordered items, with the barcode as key</param>
        private void RaisePrintItems(Dictionary<string, OrdersProduct> dict)
        {
            EventHandler<OrdersListEventArgs> handler = ItemPayed;
            if(handler != null)
            {
                handler(this, new OrdersListEventArgs(dict));
            }
        }

        /// <summary>
        /// Initiate a payment
        /// </summary>
        /// <param name="amount">The amount paid</param>
        /// <returns>the change to return as a list of (coinvalue => quantity) 
        ///          or <code>null</code> on failure</returns>
        public IDictionary<decimal, int> InitiatePayment(decimal amount)
        {
            decimal sum = CalculateAmountOfItems() - CalculateTotalDiscount();
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
            return result;
        }

        /// <summary>
        /// Prints a list of all the items on console. The items are collected through the database.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void HandleShowingProducts(object s, EventArgs e)
        {
            string title = "PRODUCTEN:";
            try
            {
                IList<IProduct> items = this.Catalog.GetAllProducts();

                Console.Out.WriteLine("======================== " + title + " =======================");
                foreach (IProduct product in items)
                {
                    Console.Out.WriteLine(String.Format("{0,4} {1,-40} {2:c}",
                        product.Barcode, product.Description, product.Amount));
                }
                Console.Out.WriteLine("=========================================================");
            }
            catch (ArgumentNullException e1)
            {
                Console.WriteLine(e1.ToString());
                throw;
            }
        }

        /// <summary>
        /// adds a scanned item to the cart when triggered
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void HandleFilledCart(object s, ItemEventArgs e)
        { 
            {
                cart.Add(e.Item);
            }
        }

        /// <summary>
        /// shows the last scanned item and total cost of all items, including the discount
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void HandleShowingScanned(object s, EventArgs e)
        {
            if (cart.Count > 0)
            {
                int productIndex = cart.Count - 1;
                decimal sum = CalculateAmountOfItems() - CalculateTotalDiscount();
                string line1 = "TOTAAL: \u20ac " + Convert.ToString(Decimal.Round(sum, 2));
                string line2 = cart[productIndex].Description;


                Console.Out.WriteLine("==========================================");
                Console.Out.WriteLine("|{0,-40}|", line1);
                Console.Out.WriteLine("|{0,-40}|", line2);
                Console.Out.WriteLine("==========================================");
            }
        }

        /// <summary>
        /// Calculates the sum of all items and rounds the decimal off to two values
        /// </summary>
        /// <returns></returns>
        public decimal CalculateAmountOfItems()
        {
            decimal sum = Decimal.Round(cart.Sum(p => p.Amount), 2);
            return sum;
        }

        public Dictionary<string, OrdersProduct> ReturnOrderedCart()
        {
            Dictionary<string, OrdersProduct> ordered = new Dictionary<string, OrdersProduct>();
            foreach (IProduct item in cart)
            {
                if (!ordered.ContainsKey(item.Barcode))
                {
                    ordered.Add(item.Barcode, new OrdersProduct(1, item.Amount));
                }
                else
                {
                    ordered[item.Barcode].Price += item.Amount;
                    ordered[item.Barcode].Amount++;
                }
            }
            return ordered;
        }

        /// <summary>
        /// Raises the event to stop counting the items eligible for a discount in DiscountManager
        /// </summary>
        private void RaiseClearDiscountCheck()
        {
            EventHandler handler = ClearDiscountCheck;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Adds the order to the database, triggers the printer event, and then clears the scanned items list (cart)
        /// </summary>
        public void AddOrder()
        {
            Dictionary<string, OrdersProduct> dict = ReturnOrderedCart();

            IPurchaseRepository repository0 = new PurchaseRepository();
            IProductRepository repository1 = new ProductRepository();
            IOrdersProductRepository repository2 = new OrdersProductRepository();
            var order = new Purchase();
            repository0.Add(order);
            foreach (string key in dict.Keys)
            {
                var product = repository1.GetByBarcode(key);
                dict[key].Item_id = product.Id;
                dict[key].Order_id = order.Id;
                //reducing the price per item by the discount 
                if (discount.ContainsKey(key))
                {
                    dict[key].Price -= discount[key];
                }
                repository2.Add(dict[key]);
            }
            RaisePrintItems(dict);
            RaiseClearDiscountCheck();
            discount.Clear();
            cart.Clear();
        }
    }
}
