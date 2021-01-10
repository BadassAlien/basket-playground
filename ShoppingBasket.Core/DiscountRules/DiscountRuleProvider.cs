using ShoppingBasket.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Core.DiscountRules
{
    public class DiscountRuleProvider : IDiscountRuleProvider
    {
        private readonly List<IDiscountRule> _rules = new List<IDiscountRule>();

        public DiscountRuleProvider(IEnumerable<IDiscountRule> discountRules)
        {
            foreach (var rule in discountRules)
            {
                AddDiscount(rule);
            }
        }

        public void AddDiscount(IDiscountRule discountRule)
        {
            var ruleExists = _rules.Any(x => x.GetType() == discountRule.GetType());
            if (ruleExists == false)
            {
                _rules.Add(discountRule);
            }
        }

        public IEnumerable<IDiscountRule> GetAvailableDiscounts()
        {
            return _rules;
        }
    }
}
