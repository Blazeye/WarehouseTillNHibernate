using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTill.model
{
    interface IOrdersProduct
    {
        int Amount { get; set; }
        decimal Price { get; set; }
    }
}
