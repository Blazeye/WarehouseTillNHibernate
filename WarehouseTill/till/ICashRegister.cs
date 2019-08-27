using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTill.till
{
    public interface ICashRegister
    {
        IDictionary<decimal, int> Register { get; set; }
        IDictionary<decimal, int> RegisterStartDay { get; }

        IDictionary<decimal, int> MakeChange(decimal itemCost, decimal billsUsed);

        decimal CountRegister();

        decimal ProcessEndOfDay();
    }
}
