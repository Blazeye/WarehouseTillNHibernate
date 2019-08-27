using System.Collections.Generic;
using WarehouseTill.display;
using WarehouseTill.products;

namespace WarehouseTill
{
    /// <summary>
    /// Test implementation of the ITillDisplay interface for NUnit testing
    /// </summary>
    internal class TestDisplay : ITillDisplay
    {
        public string ReceivedLine1 { get; private set; }
        public string ReceivedLine2 { get; private set; }
        public string ReceivedTitle { get; private set; }
        public IList<IProduct> ReceivedProducts { get; private set; }

        public void DisplayClientScreen(string line1, string line2) {
            ReceivedLine1 = line1;
            ReceivedLine2 = line2;
        }

        public void DisplayProducts(string title, IList<IProduct> products) {
            ReceivedTitle = title;
            ReceivedProducts = products;
        }
    }
}