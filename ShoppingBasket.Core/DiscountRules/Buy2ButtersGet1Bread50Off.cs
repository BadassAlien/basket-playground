using ShoppingBasket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Core.DiscountRules
{
    public class Buy2ButtersGet1Bread50Off : IDiscountRule
    {
        public string Description { get; } = "Buy 2 butters get one bread at 50% off";

        public double ApplyDiscount(IEnumerable<IBasketItem> basketItems)
        {
            if (IsApplicable(basketItems) == false)
            {
                return 0;
            }

            var butter = basketItems.FirstOrDefault(x => x.Product.Name.Equals("butter", StringComparison.OrdinalIgnoreCase));
            var bread = basketItems.FirstOrDefault(x => x.Product.Name.Equals("bread", StringComparison.OrdinalIgnoreCase));

            int butterCount = butter?.Quantity / 2 ?? 0;

            // If understood correctly the discount can only be applied once per bread
            double result = 0;
            for (var i = 0; i < butterCount; i++)
            {
                if (i + 1 > bread?.Quantity)
                {
                    break;
                }
                result += bread?.Product?.Price * 0.5 ?? 0;
            }

            return Math.Round(result, 2);

        }

        public bool IsApplicable(IEnumerable<IBasketItem> basketItems)
        {
            if (basketItems == null || !basketItems.Any())
            {
                return false;
            }
            var butter = basketItems.FirstOrDefault(x => x.Product.Name.Equals("butter", StringComparison.OrdinalIgnoreCase));
            var bread = basketItems.FirstOrDefault(x => x.Product.Name.Equals("bread", StringComparison.OrdinalIgnoreCase));

            return butter != null && bread != null && butter.Quantity >= 2;


        }
    }
}
