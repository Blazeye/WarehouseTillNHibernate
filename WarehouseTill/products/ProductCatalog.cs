using System.Collections.Generic;
using WarehouseTill.repository;

namespace WarehouseTill.products
{
    public class ProductCatalog : IProductCatalog
    {
        public List<IProduct> ProductList { get; }

        /// <summary>
        /// Constructor which initializes 4 products
        /// </summary>
        public ProductCatalog()
        {
            // Add 4 example products to the internal data structure of this catalog here
            ProductList = new List<IProduct>();
            IProductRepository repository = new ProductRepository();
            ProductList = repository.GetAllProducts();

        }

        /// <summary>
        /// Find a product for a barcode
        /// </summary>
        /// <returns>the product or <c>null</c> if not found</returns>
        public IProduct FindProductForBarcode(string barcode)
        {

            Product rightProduct = null;

            foreach (Product item in ProductList)
            {
                if (item.Barcode == barcode)
                {
                    rightProduct = item;
                    break;
                }
            }
            return rightProduct;
        }

        /// <summary>
        /// returns a list of all products in the catalog
        /// </summary>
        /// <returns>a list of all products</returns>
        public IList<IProduct> GetAllProducts()
        {

            IList<IProduct> listProducts;
            listProducts = (IList<IProduct>)this.ProductList;

            //return this.ProductList;
            return listProducts;

        }
    }
}
