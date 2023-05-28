using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ProductsLib
{
    public abstract class Product
    {
        public Product()
        {
            
        }

        protected Product(string description, decimal sellPrice, decimal buyPrice)
        {
            Description = description;
            SellPrice = sellPrice;
            BuyPrice = buyPrice;
        }

        public long Id { get; set; }

        public string Description { get; set; }

        public decimal SellPrice { get; set; }

        public decimal BuyPrice { get; set; }

        public int Quantity { get; set; }

        public virtual string GetDescription()
        {
            return $"Id: {Id}, Description: {Description}, Sell price: {SellPrice}, Buy price: {BuyPrice}, Quantity: {Quantity} ";
        }

        public abstract string GetProductType();
    }
}
