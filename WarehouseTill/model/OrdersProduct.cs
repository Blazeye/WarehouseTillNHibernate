using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTill.model
{
    public class OrdersProduct
    {
        public virtual int Id { get; set; }
        public virtual int Item_id { get; set; }
        public virtual int Order_id { get; set; }
        public virtual int Amount { get; set; }
        public virtual decimal Price { get; set; }

        protected OrdersProduct()
        {
            //This is needed for NHibernate
        }

        public OrdersProduct(int amount, decimal price)
        {
            this.Amount = amount;
            this.Price = price;
        }
    }
}
