using ShoppingBasket.Core.DiscountRules;
using ShoppingBasket.Core.Interfaces;
using ShoppingBasket.Core.Models;
using System;
using System.Collections.Generic;

namespace ShoppingBasketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Shopping cart demo");

            var ruleProvider = new DiscountRuleProvider(new List<IDiscountRule>() {
                new Buy3MilksGet4thFreeRule(),
                new Buy2ButtersGet1Bread50Off()
            });
            var analyticsLog = new AnalyticsConsoleLog();

            var basket = new Basket(analyticsLog, ruleProvider);

            var milk = new Product() { Name = "Milk", Price = 1.15 };
            var bread = new Product() { Name = "Bread", Price = 1 };
            var butter = new Product() { Name = "Butter", Price = 0.8 };

            basket.AddToBasket(milk, 8);
            basket.AddToBasket(bread, 1);
            basket.AddToBasket(butter, 2);

            basket.GetTotal();
            Console.ReadLine();
        }
    }
}
