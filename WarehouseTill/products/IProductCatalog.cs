using System.Collections.Generic;

namespace WarehouseTill.products
{
    public interface IProductCatalog
    {
        /// <summary>
        /// Find a product for a barcode
        /// </summary>
        /// <returns>the product or <c>null</c> if not found</returns>
        /// 

        Product FindProductForBarcode(string barcode);

        /// <returns>a list of all products</returns>
        IList<IProduct> GetAllProducts();
    }
}