using System.Collections.Generic;

namespace ShoppingBasket.Core.Interfaces
{
    public interface IDiscountRule
    {
        string Description { get; }
        double ApplyDiscount(IEnumerable<IBasketItem> basketItems);
        bool IsApplicable(IEnumerable<IBasketItem> basketItems);
    }
}
