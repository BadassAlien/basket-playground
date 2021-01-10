using ShoppingBasket.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Core.Models
{
    public class Basket : IBasket
    {
        private readonly IAnalyticsLog _log;
        private readonly IDiscountRuleProvider _discountsProvider;
        private readonly List<IDiscountRule> _applicableDiscounts = new List<IDiscountRule>();
        private readonly List<IBasketItem> _items = new List<IBasketItem>();

        public Basket(IAnalyticsLog log, IDiscountRuleProvider discountsProvider)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _discountsProvider = discountsProvider ?? throw new ArgumentNullException(nameof(discountsProvider));
        }

        public IEnumerable<IDiscountRule> Discounts
        {
            get { return _applicableDiscounts; }
        }

        public IEnumerable<IBasketItem> Items
        {
            get { return _items; }
        }

        public void AddToBasket(IProduct product, int quantity)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            var existing = _items.FirstOrDefault(x => x.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
            if (existing == null)
            {
                existing = new BasketItem(product, quantity);
                _items.Add(existing);
            }
            else
            {
                existing.Quantity += quantity;
            }
        }

        public double GetTotal()
        {
            var total = _items.Sum(x => x.Product.Price * x.Quantity);

            var rules = _discountsProvider.GetAvailableDiscounts();
            foreach (var rule in rules)
            {
                if (!rule.IsApplicable(_items))
                    continue;

                var discount = rule.ApplyDiscount(_items);
                total -= discount;
                _applicableDiscounts.Add(rule);
            }
            var result = Math.Round(total, 2);

            _log.Log(this, result);

            return result;
        }
    }
}
