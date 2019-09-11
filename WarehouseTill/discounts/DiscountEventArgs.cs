using System;
using System.Collections.Generic;
using WarehouseTill.products;

namespace WarehouseTill.discounts
{
    public class DiscountEventArgs : EventArgs
    {
        public IProduct Item { get; }
        public int Number { get; }
        public decimal Percentage { get; }

        public DiscountEventArgs(IProduct item, int number, decimal percentage)
        {
            Item = item;
            Number = number;
            Percentage = percentage;
        }
    }
}
