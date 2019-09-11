using WarehouseTill.products;

namespace WarehouseTill
{
    /// <summary>
    /// Test implementation of the IProduct interface for NUnit testing
    /// </summary>
    internal class TestProduct : IProduct {

        public string Barcode { get; }
        public string Description { get; }
        public decimal Amount { get; set; }

        public TestProduct(int productId) {
            Barcode = productId.ToString();
            Description = string.Format("Test product #{0}", productId);
            Amount = productId;
        }
    }
}