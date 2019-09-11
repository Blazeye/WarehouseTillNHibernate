using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseTill.products;
using WarehouseTill.warehouse;

namespace WarehouseTill.discounts
{
    public class DiscountManager
    {
        private Dictionary<IProduct, int> DiscountCheckList = new Dictionary<IProduct, int>();
        public event EventHandler<DiscountEventArgs> ItemDiscounted;

        public Dictionary<IProduct, int> GetProductList()
        {
            return DiscountCheckList;
        }

        /// <summary>
        /// triggers the event to keep count of the discounts
        /// </summary>
        /// <param name="product"></param>
        public void RaiseAddDiscount(IProduct product, int number, decimal percentage)
        {
            EventHandler<DiscountEventArgs> handler = ItemDiscounted;
            if (handler != null)
            {
                handler(this, new DiscountEventArgs(product, number, percentage));
            }
        }

        public void HandleClearDiscountCheck(object s, EventArgs e)
        {
            DiscountCheckList.Clear();
        }

        /// <summary>
        /// checks whether an item is elligible for a discount, and triggers a new event if so.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e">scanned item</param>
        public void HandleCheckDiscount(object s, ItemEventArgs e)
        {
            if (DiscountCheckList.ContainsKey(e.Item))
            {
                DiscountCheckList[e.Item] += 1;
                //Discount voor opdracht5
                if (DiscountCheckList[e.Item] % 3 == 0)
                {
                    RaiseAddDiscount(e.Item, 3, .25m);
                }
                //Discount voor uitbreiding 1 van opdracht 5
                if (DiscountCheckList[e.Item] % 4 == 0 && (e.Item.Barcode == "9902" || e.Item.Barcode == "3568"))
                {
                    RaiseAddDiscount(e.Item, 4, .10m);
                }
                //Discount voor uitbreiding 2 van opdracht 5
                if (DiscountCheckList[e.Item] % 5 == 0 &&
                    (e.Item.Barcode == "9902" || e.Item.Barcode == "3568" || e.Item.Barcode == "7324"))
                {
                    var KeyR = DiscountCheckList.Keys.OrderBy(item => item.Amount).First();
                    RaiseAddDiscount(KeyR, 5, .05m);
                }
            }
            else
            {
                DiscountCheckList.Add(e.Item, 1);
            }
            //Discount voor uitbreiding 3 van opdracht 5
            if (e.Item.Barcode == "9902" || e.Item.Barcode == "1234")
            {
                var item1 = DiscountCheckList.Keys.Where(thng => thng.Barcode == "9902").FirstOrDefault();
                var item2 = DiscountCheckList.Keys.Where(thng => thng.Barcode == "1234").FirstOrDefault();
                if (item1 == null || item2 == null)
                {
                    return;
                }
                int min = Math.Min(DiscountCheckList[item1], DiscountCheckList[item2]);
                if (DiscountCheckList[e.Item] == min)
                {
                    RaiseAddDiscount(item1, 1, .10m);
                    RaiseAddDiscount(item2, 1, .10m);
                }
            }

        }
    }
}
