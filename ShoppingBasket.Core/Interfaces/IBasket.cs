using System.Collections.Generic;

namespace ShoppingBasket.Core.Interfaces
{
    public interface IBasket
    {
        void AddToBasket(IProduct product, int quantity);

        double GetTotal();

        IEnumerable<IDiscountRule> Discounts { get; }

        IEnumerable<IBasketItem> Items { get; }
    }
}
