using System.Collections.Generic;
using WarehouseTill.products;

namespace WarehouseTill.warehouse
{
    public class Warehouse
    {

        public List<IProduct> ProductList = new List<IProduct>();

        public List<IProduct> GetProductList()
        {
            return ProductList;
        }


        public void HandleAddItem(object s, ItemEventArgs e)
        {
            ProductList.Add(e.Item);
        }
    }
}
