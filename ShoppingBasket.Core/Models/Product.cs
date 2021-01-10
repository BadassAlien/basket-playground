using ShoppingBasket.Core.Interfaces;

namespace ShoppingBasket.Core.Models
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
