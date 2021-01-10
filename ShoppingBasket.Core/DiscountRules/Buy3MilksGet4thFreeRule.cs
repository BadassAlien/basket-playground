using ShoppingBasket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Core.DiscountRules
{
    public class Buy3MilksGet4thFreeRule : IDiscountRule
    {
        public string Description { get; } = "Buy 3 milks get 4th milk for free";


        public double ApplyDiscount(IEnumerable<IBasketItem> basketItems)
        {
            if (IsApplicable(basketItems) == false)
            {
                return 0;
            }

            var milkItem = basketItems.FirstOrDefault(x => x.Product.Name.Equals("milk", StringComparison.OrdinalIgnoreCase));
            var milkPrice = milkItem.Product.Price;
            int milksCount = milkItem.Quantity / 4;

            return milkPrice * milksCount;

        }

        public bool IsApplicable(IEnumerable<IBasketItem> basketItems)
        {
            if (basketItems == null || !basketItems.Any())
            {
                return false;
            }
            return basketItems.FirstOrDefault(x => x.Product.Name.Equals("milk", StringComparison.OrdinalIgnoreCase))?.Quantity > 3;
        }
    }
}
