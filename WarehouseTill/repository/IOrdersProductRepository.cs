using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTill.model;

namespace WarehouseTill.repository
{
    public interface IOrdersProductRepository
    {
        void Add(OrdersProduct ordersProduct);
    }
}
