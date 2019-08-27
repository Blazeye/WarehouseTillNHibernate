using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseTill.products
{
    public class Product : IProduct
    {
        public virtual int Id { get; set; }
        public virtual string Barcode { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Amount { get; set; }

        protected Product()
        {
            // Needed for Nhibernate
        }
        public Product(string barcode, string description, decimal amount)
        {

            if (barcode.Length > 4)
            {
                throw new ArgumentException("only barcodes upto 4 characters are allowed", nameof(barcode));
            }
            if (amount != Decimal.Round(amount, 3))
            {
                throw new ArgumentException("supply a persision of 3 decimals", nameof(amount));
            }

            this.Barcode = barcode;
            this.Description = description;
            this.Amount = amount;
        }
        public override bool Equals(object obj)
        {
            Product otherProduct = obj as Product;
            if (otherProduct == null)
            {
                return false;
            }
            if (otherProduct.Id == 0 || Id == 0)
            {
                return otherProduct.Barcode == Barcode &&
                       otherProduct.Amount == Amount &&
                       otherProduct.Description == Description;
            }

            return otherProduct.Id == Id;
        }
    }
}
