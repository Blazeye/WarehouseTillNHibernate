namespace WarehouseTill.products
{
    public interface IProduct
    {
        /// <summary>
        /// Barcode of 4 digits
        /// </summary>
        string Barcode { get; }
        /// <summary>
        /// Description of the product (max 28 characters)
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Amount in euro's
        /// </summary>
        decimal Amount { get; }
    }
}