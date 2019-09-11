using System;
using System.Collections.Generic;
using WarehouseTill.products;

namespace WarehouseTill.warehouse
{
    public class ItemEventArgs : EventArgs
    {
        public IProduct Item { get; }

        public ItemEventArgs(IProduct item)
        {
            Item = item;
        }
    }
}
