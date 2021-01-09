using Xunit;
using Moq;
using AppSportsStore.Controllers;
using AppSportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SportsStore.Tests
{
    public class HomeControllerTest
    {
        private Mock<IStoreRepo> mock = new Mock<IStoreRepo>();
        [Fact]
        public void IndexReturnStaticData()
        {
            //Given
            mock.SetupGet(r => r.Products).Returns(SeedData.GetData());
            HomeController controller = new HomeController(mock.Object);
            //When
            var Index = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            //Then
            Assert.Equal(SeedData.GetData(), Index,
            Comparer.Get<Product>((x, y) => x.Price == y.Price &&
                                            x.Name == y.Name && x.Category == y.Category));
            mock.VerifyGet(m => m.Products, Times.Once);
        }
        [Fact]
        public void PriceLessThanZero()
        {
            //Given
            var controller = new HomeController(mock.Object);
            mock.SetupGet(x => x.Products).Returns(SeedData.GetData());
            //When
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            //Then
            Assert.DoesNotContain(model, x => x.Price <= 0);
        }
        [Fact]
        public void CountProductsInPages()
        {
            // Given
            mock.SetupGet(x => x.Products).Returns(SeedData.GetData());
            var controller = new HomeController(mock.Object);
            var prods = new List<Product>();
            //When
            for (int i = 1; (i - 1) * controller.PageSize < mock.Object.Products.Count(); i++)
            {
                var products = (controller.ProductsPagination(i) as ViewResult)?.ViewData.Model as IEnumerable<Product>;
                prods.AddRange(products);
            }
            //Then
            Assert.Equal(mock.Object.Products.Count(), prods.Count());
            Assert.Equal(SeedData.GetData(), prods,
                         Comparer.Get<Product>((x, y) => x.ProductID == y.ProductID &&
                                                         x.Price == y.Price &&
                                                         x.Name == y.Name));
        }
    }
}