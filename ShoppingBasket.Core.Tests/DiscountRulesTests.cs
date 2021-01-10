using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Core.DiscountRules;
using ShoppingBasket.Core.Interfaces;
using ShoppingBasket.Core.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Core.Tests
{
    [TestClass]
    public class DiscountRulesTests
    {

        [TestMethod]
        public void Buy2ButtersGet1Bread50Off_IsApplicable_ShouldReturn_Correctly()
        {
            List<IBasketItem> basketItems = null;
            var rule = new Buy2ButtersGet1Bread50Off();
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems = new List<IBasketItem>();
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems.Add(new BasketItem(new Product() { Name = "Milk", Price = 1.15 }, 1));
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems.Add(new BasketItem(new Product() { Name = "Bread", Price = 1 }, 1));
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems.Add(new BasketItem(new Product() { Name = "Butter", Price = 0.8 }, 1));
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems[2].Quantity++;
            Assert.IsTrue(rule.IsApplicable(basketItems));
        }

        [TestMethod]
        public void Buy2ButtersGet1Bread50Off_ApplyDiscount_ShouldApply_Correctly()
        {
            List<IBasketItem> basketItems = null;
            var rule = new Buy2ButtersGet1Bread50Off();
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems = new List<IBasketItem>();
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems.Add(new BasketItem(new Product() { Name = "Milk", Price = 1.15 }, 1));
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems.Add(new BasketItem(new Product() { Name = "Bread", Price = 1 }, 1));
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems.Add(new BasketItem(new Product() { Name = "Butter", Price = 0.8 }, 1));
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems[2].Quantity++;
            Assert.AreEqual(0.5, rule.ApplyDiscount(basketItems));
        }

        [TestMethod]
        public void Buy3MilksGet4thFreeRule_IsApplicable_ShouldReturn_Correctly()
        {
            List<IBasketItem> basketItems = null;
            var rule = new Buy3MilksGet4thFreeRule();
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems = new List<IBasketItem>();
            Assert.IsFalse(rule.IsApplicable(basketItems));

            var milk = new Product()
            {
                Name = "Milk",
                Price = 1.15
            };


            basketItems.Add(new BasketItem(milk, 1));
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems[0].Quantity++;
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems[0].Quantity++;
            Assert.IsFalse(rule.IsApplicable(basketItems));

            basketItems[0].Quantity++;
            Assert.IsTrue(rule.IsApplicable(basketItems));
        }

        [TestMethod]
        public void Buy3MilksGet4thFreeRule_ApplyDiscount_ShouldApply_Correctly()
        {
            List<IBasketItem> basketItems = null;
            var rule = new Buy3MilksGet4thFreeRule();
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems = new List<IBasketItem>();
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            var milk = new Product()
            {
                Name = "Milk",
                Price = 1.15
            };


            basketItems.Add(new BasketItem(milk, 1));
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems[0].Quantity++;
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems[0].Quantity++;
            Assert.AreEqual(0, rule.ApplyDiscount(basketItems));

            basketItems[0].Quantity++;
            Assert.AreEqual(1.15, rule.ApplyDiscount(basketItems));
        }
    }
}
