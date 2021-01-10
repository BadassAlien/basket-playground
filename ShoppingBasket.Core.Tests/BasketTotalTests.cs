using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingBasket.Core.DiscountRules;
using ShoppingBasket.Core.Interfaces;
using ShoppingBasket.Core.Models;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Core.Tests
{
    [TestClass]
    public class BasketTests
    {
        private readonly IBasket _basket;

        public BasketTests()
        {
            var log = new Mock<IAnalyticsLog>();
            var drp = new Mock<IDiscountRuleProvider>();
            drp.Setup(x => x.GetAvailableDiscounts()).Returns(
             new List<IDiscountRule>()
            {
                new Buy2ButtersGet1Bread50Off(),
                new Buy3MilksGet4thFreeRule(),
            });
            _basket = new Basket(log.Object, drp.Object);
        }

        [TestMethod]
        public void GetTotal_1Bread_1Butter_1Milk_Result_2_95()
        {
            _basket.AddToBasket(new Product() { Name = "Milk", Price = 1.15 }, 1);
            _basket.AddToBasket(new Product() { Name = "Bread", Price = 1 }, 1);
            _basket.AddToBasket(new Product() { Name = "Butter", Price = 0.8 }, 1);

            var total = _basket.GetTotal();
            Assert.AreEqual(2.95, total);

        }

        [TestMethod]
        public void GetTotal_2Breads_2Butters_Result_3_10()
        {
            _basket.AddToBasket(new Product() { Name = "Bread", Price = 1 }, 2);
            _basket.AddToBasket(new Product() { Name = "Butter", Price = 0.8 }, 2);

            var total = _basket.GetTotal();
            Assert.AreEqual(3.10, total);
        }

        [TestMethod]
        public void GetTotal_2Breads_4Butters_Result_4_60()
        {
            _basket.AddToBasket(new Product() { Name = "Bread", Price = 1 }, 2);
            _basket.AddToBasket(new Product() { Name = "Butter", Price = 0.8 }, 4);

            var total = _basket.GetTotal();
            Assert.AreEqual(4.20, total);
        }

        [TestMethod]
        public void GetTotal_1Breads_4Butters_Result_3_70()
        {
            _basket.AddToBasket(new Product() { Name = "Bread", Price = 1 }, 1);
            _basket.AddToBasket(new Product() { Name = "Butter", Price = 0.8 }, 4);

            var total = _basket.GetTotal();
            Assert.AreEqual(3.70, total);
        }

        [DataTestMethod]
        [DataRow(1, DisplayName = "1 milk")]
        [DataRow(2, DisplayName = "2 milk")]
        [DataRow(3, DisplayName = "3 milk")]
        [DataRow(4, DisplayName = "4 milk")]
        [DataRow(5, DisplayName = "5 milk")]
        [DataRow(8, DisplayName = "8 milk")]
        [DataRow(9, DisplayName = "9 milk")]
        public void GetTotal_Milks_Result_Correct(int numberOfMilk)
        {
            var product = new Product() { Name = "Milk", Price = 1.15 };
            _basket.AddToBasket(product, numberOfMilk);

            var total = _basket.GetTotal();
            var expected = Math.Round(product.Price * numberOfMilk - (numberOfMilk / 4 * product.Price), 2);
            Assert.AreEqual(expected, total);
        }
        [TestMethod]
        public void GetTotal_4Milks_Result_3_45()
        {
            _basket.AddToBasket(new Product() { Name = "Milk", Price = 1.15 }, 4);

            var total = _basket.GetTotal();
            Assert.AreEqual(3.45, total);
        }

        [TestMethod]
        public void GetTotal_1Bread_2Butters_8Milk_Result_9()
        {
            _basket.AddToBasket(new Product() { Name = "Milk", Price = 1.15 }, 8);
            _basket.AddToBasket(new Product() { Name = "Bread", Price = 1 }, 1);
            _basket.AddToBasket(new Product() { Name = "Butter", Price = 0.8 }, 2);

            var total = _basket.GetTotal();
            Assert.AreEqual(9, total);
        }
    }
}
