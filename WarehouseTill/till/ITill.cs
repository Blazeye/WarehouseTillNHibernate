using WarehouseTill.display;
using System.Collections.Generic;
using WarehouseTill.products;

namespace WarehouseTill.till
{
    /// <summary>
    /// Interface every till needs to implement
    /// </summary>
    public interface ITill
    {
        List<IProduct> cart { get; set; }
        /// <summary>
        /// Installs an interface to be used when displaying
        /// </summary>
        /// <param name="tillDisplay">The interface to use from now on</param>
        void SetDisplayInterface(ITillDisplay tillDisplay);

        /// <summary>
        /// Handle a scan of a barcode
        /// </summary>
        /// <param name="barcode">The scanned barcode</param>
        /// <returns><c>true</c> if succesfull, <c>false</c> otherwise</returns>
        bool HandleBarcode(string barcode);

        /// <summary>
        /// Initiate a payment
        /// </summary>
        /// <param name="amount">The amount paid</param>
        /// <returns>the change to return or null on failure</returns>
        IDictionary<decimal, int> InitiatePayment(decimal amount);

        /// <summary>
        /// Show all products
        /// </summary>
        void ShowAllProducts();

        void ShowScannedItems();
        void AddOrder();
    }
}