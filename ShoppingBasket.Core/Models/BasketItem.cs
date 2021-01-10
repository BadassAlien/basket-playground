using ShoppingBasket.Core.Interfaces;

namespace ShoppingBasket.Core.Models
{
    public class BasketItem : IBasketItem
    {
        public BasketItem(IProduct product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public IProduct Product { get; }

        public int Quantity { get; set; }
    }
}
