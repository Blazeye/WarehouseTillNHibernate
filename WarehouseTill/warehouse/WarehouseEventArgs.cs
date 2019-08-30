using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTill.products;

namespace WarehouseTill.warehouse
{
    class WarehouseEventArgs : EventArgs
    {
        public List<IProduct> ProductList { get; protected set; }
        public WarehouseEventArgs(List<IProduct> productList)
        {
            this.ProductList = productList;
        }
    }
}
