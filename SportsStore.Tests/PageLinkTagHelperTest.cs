using System.Collections.Generic;
using System.Threading.Tasks;
using AppSportsStore.Infrastructure;
using AppSportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace SportsStore.Tests
{
    public class PageLinkTagHelperTest
    {
        [Fact]
        public void PageLinkTagHelper()
        {
            //Given
            var url = new Mock<IUrlHelper>();
            url.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
               .Returns("Test/Page1")
               .Returns("Test/Page2")
               .Returns("Test/Page3");
            var urlFactory = new Mock<IUrlHelperFactory>();
            urlFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(url.Object);
            PageLinkTagHelper helper = new PageLinkTagHelper(urlFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };
            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");
            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));
            //When
            helper.Process(ctx, output);
            var c = output.Content.GetContent();
            //Then
            Assert.Equal(@"<a href=""Test/Page1"">1</a>"
                       + @"<a href=""Test/Page2"">2</a>"
                       + @"<a href=""Test/Page3"">3</a>",
                       output.Content.GetContent());
        }
    }
}