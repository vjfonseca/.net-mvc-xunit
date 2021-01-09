using Xunit;
using Moq;
using AppSportsStore.Controllers;
using AppSportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SportsStore.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexReturnStaticData()
        {
            //Given
            var mock = new Mock<IStoreRepo>();
            mock.SetupGet(r => r.Products).Returns(SeedData.GetData());
            var controller = new HomeController(mock.Object);
            //When
            var Index = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            //Then
            var d = SeedData.GetData();
            var a = Index;
            Assert.Equal(SeedData.GetData(), Index,
            Comparer.Get<Product>((x, y) => x.Price == y.Price &&
                                            x.Name == y.Name && x.Category == y.Category));
            mock.VerifyGet(m => m.Products, Times.Once);
        }
        [Fact]
        public void PriceLessThenZero()
        {
            //Given
            var mock = new Mock<IStoreRepo>();
            var controller = new HomeController(mock.Object);
            mock.SetupGet(x => x.Products).Returns(SeedData.GetData());
            //When
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            //Then
            Assert.DoesNotContain(model, x => x.Price <= 0);
        }
    }
}