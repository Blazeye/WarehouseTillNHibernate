using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTill.model
{
    public interface IPurchase
    {

        string Date { get; set; }
        int Id { get; set; }
    }
}
