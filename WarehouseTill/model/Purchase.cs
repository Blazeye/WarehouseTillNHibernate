using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTill.model
{
    public class Purchase : IPurchase
    {
        public virtual string Date { get; set; }
        public virtual int Id { get; set; }

        public Purchase()
        {
            this.Date = DateTime.Now.ToString("d/M/yyyy");
        }
        public override bool Equals(object obj)
        {
            Purchase otherPurchase = obj as Purchase;
            if (otherPurchase == null)
            {
                return false;
            }
            if (otherPurchase.Id == 0 || Id == 0)
            {
                return otherPurchase.Date == Date;
            }

            return otherPurchase.Id == Id;
        }
    }
}
