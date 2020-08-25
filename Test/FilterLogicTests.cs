using System;
using Xunit;
using FilterApiApplication.Models;
using FilterApiApplication.Code;
using System.Collections.Generic;
using System.Linq;

namespace Test
{  
    public class FilterLogicTests
    {
        #region test products

        public static Product testProduct1 = new Product
            ("Title1",1, new List<string> { "small", "medium" }, "Description1 red green purple yellow black");

        public static Product testProduct2 = new Product
            ("Title2", 2, new List<string> { "medium", "large" }, "Description2 red green purple yellow black");

        public static Product testProduct3 = new Product
            ("Title3", 3, new List<string> { "large" }, "Description3 red green purple yellow black");

        #endregion test products

        [Fact]
        public void TestCheckIfFilterListIsEmpty()
        {
            var products = new List<Product>();

            Assert.True(FilterLogic.CheckIfFilterListIsEmpty(products));

            products.Add(testProduct1);
            products.Add(testProduct2);

            Assert.False(FilterLogic.CheckIfFilterListIsEmpty(products));
        }

        [Fact]
        public void TestFilterProductsByMaxPrice()
        {
            var products = new List<Product>();

            products.Add(testProduct1);
            products.Add(testProduct2);

            products = FilterLogic.FilterProductsByMaxPrice(products, 1);
            Assert.Single(products);
        }

        [Fact]
        public void TestFilterProductsBySize()
        {
            var products = new List<Product>();

            products.Add(testProduct1);
            products.Add(testProduct2);

            products = FilterLogic.FilterProductsBySize(products, "small");
            Assert.Single(products);
        }

        [Fact]
        public void TestFindAllSizesFromProductList()
        {
            var products = new List<Product>();

            products.Add(testProduct1);
            products.Add(testProduct2);

            var allSizes = FilterLogic.FindAllSizesFromProductList(products);
            allSizes = allSizes.OrderByDescending(x => x).ToList();

            Assert.Equal(new List<string> { "small","medium","large" }, allSizes);
        }

        [Fact]
        public void TestFindCommonWordsFromProductList()
        {
            var products = new List<Product>();

            products.Add(testProduct1);
            products.Add(testProduct2);

            var commonWords = FilterLogic.FindCommonWordsFromProductList(products);
            commonWords = commonWords.OrderBy(x => x).ToList();

            Assert.Equal(new List<string> { "Description1", "Description2" }, commonWords);
        }

        [Fact]
        public void TestHighlightWordsInDescription()
        {
            var products = new List<Product>();

            products.Add(testProduct1);
            products.Add(testProduct3);

            products = FilterLogic.HighlightWordsInDescription(products,"Description3");

            Assert.Contains("<em>Description3</em>",products.LastOrDefault().Description);
        }

        [Fact]
        public void TestFindMinPriceFromProductList()
        {
            var products = new List<Product>();

            products.Add(testProduct1);
            products.Add(testProduct2);

            var price = FilterLogic.FindMinPriceFromProductList(products);

            Assert.True(price == 1);
        }

        [Fact]
        public void TestFindMaxPriceFromProductList()
        {
            var products = new List<Product>();

            products.Add(testProduct1);
            products.Add(testProduct2);

            var price = FilterLogic.FindMaxPriceFromProductList(products);

            Assert.True(price == 2);
        }
    }
}
