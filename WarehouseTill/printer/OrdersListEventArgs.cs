using System;
using System.Collections.Generic;
using WarehouseTill.model;

namespace WarehouseTill.printer
{
    public class OrdersListEventArgs : EventArgs
    {
        public Dictionary<string, OrdersProduct> OrderList { get; set; }
        public OrdersListEventArgs(Dictionary<string, OrdersProduct> orderList)
        {
            this.OrderList = orderList;
        }
    }
}
