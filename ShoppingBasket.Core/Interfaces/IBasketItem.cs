namespace ShoppingBasket.Core.Interfaces
{
    public interface IBasketItem
    {
        IProduct Product { get; }
        int Quantity { get; set; }
    }
}
