using ShoppingBasket.Core.Interfaces;
using System;

namespace ShoppingBasket.Core.Models
{
    public class AnalyticsConsoleLog : IAnalyticsLog
    {
        public void Log(IBasket basket, double total)
        {
            Console.WriteLine("Shopping basket");
            Console.WriteLine("Product\t Price\t Quantity");
            foreach (var item in basket.Items)
            {
                Console.WriteLine($"{item.Product.Name}\t {item.Product.Price}\t {item.Quantity}");
            }

            Console.WriteLine("Applied discount\t Amount");
            foreach (var discount in basket.Discounts)
            {
                Console.WriteLine($"{discount.Description}\t {discount.ApplyDiscount(basket.Items)}");
            }

            Console.WriteLine($"Total {total}");
        }
    }
}
