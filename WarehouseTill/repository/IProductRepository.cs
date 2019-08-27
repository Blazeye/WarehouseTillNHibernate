using System.Collections.Generic;
using WarehouseTill.products;

namespace WarehouseTill.repository
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
        Product GetById(int productId);
        Product GetByBarcode(string barcode);
        List<IProduct> GetAllProducts();
    }
}

