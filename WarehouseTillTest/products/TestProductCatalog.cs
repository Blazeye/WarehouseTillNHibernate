using System.Collections.Generic;
using WarehouseTill.products;

namespace WarehouseTill
{
    /// <summary>
    /// Test implementation of the IProductCatalog interface for NUnit testing
    /// </summary>
    internal class TestProductCatalogus : IProductCatalog
    {
        /// <summary>
        /// Can be set in the preperation step 
        /// </summary>
        public IProduct ProductToReturn { get; set; } = null;

        /// <summary>
        /// List of test products
        /// </summary>
        public List<IProduct> ProductList { get; } = new List<IProduct> { new TestProduct(1), new TestProduct(3) };

        // interface functions 

        IProduct IProductCatalog.FindProductForBarcode(string barcode) {
            return ProductToReturn;
        }

        IList<IProduct> IProductCatalog.GetAllProducts() {
            return ProductList;
        }
    }
}