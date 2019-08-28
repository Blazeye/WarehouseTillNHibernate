using System;
using System.Collections.Generic;
using WarehouseTill.display;
using WarehouseTill.products;
using WarehouseTill.till;

namespace EventsTillTest.till
{
    internal class TestTill : ITill
    {
        public bool BarcodeToReturn { get; set; } = false;
        public IDictionary<decimal, int> ChangeToReturn = null;
        public List<IProduct> cart { get; set; } = new List<IProduct>();
        private decimal sum { get; set; } = 0;
        private IProductCatalog List { get; set; } = null;
        private ITillDisplay Display { get; set; } = null;
 

        public void SetDisplayInterface(ITillDisplay a)
        {

        }
        public bool HandleBarcode(string barcode)
        {
            return BarcodeToReturn;
        }
        public IDictionary<decimal, int> InitiatePayment(decimal amount)
        {
            return ChangeToReturn;
        }

        public void ShowAllProducts()
        {

        }

        public void ShowScannedItems()
        {
            
        }
        public void AddOrder()
        {

        }
    }
}
