using WarehouseTill.products;
using System.Collections.Generic;

namespace WarehouseTill.display
{
    /// <summary>
    /// Interface that every object that wants to display something for the till needs to implement
    /// </summary>
    public interface ITillDisplay
    {
        /// <summary>
        /// Display all produducts
        /// </summary>
        /// <param name="title">The title of the list</param>
        /// <param name="products">The list of products to show</param>
        void DisplayProducts(string title, IList<IProduct> products);

        /// <summary>
        /// Display the lines of the client display
        /// </summary>
        /// <param name="line1">first line (max 40 characters)</param>
        /// <param name="line2">second line (max 40 characters)</param>
        void DisplayClientScreen(string line1, string line2);
    }
}
