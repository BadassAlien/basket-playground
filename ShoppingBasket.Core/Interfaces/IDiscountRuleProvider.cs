using System.Collections.Generic;

namespace ShoppingBasket.Core.Interfaces
{
    public interface IDiscountRuleProvider
    {
        IEnumerable<IDiscountRule> GetAvailableDiscounts();

        void AddDiscount(IDiscountRule discountRule);
    }
}
