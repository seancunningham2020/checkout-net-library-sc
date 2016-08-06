using FluentAssertions;
using NUnit.Framework;
using System.Net;

namespace Tests.ShoppingListService
{
    [TestFixture]
    public class ShoppingListServiceTests : BaseServiceTests
    {
        [Test]
        public void ShoppingListService_GetProduct_ReturnsProduct()
        {
            // When
            var response = CheckoutClient.ShoppingListService.GetProduct("Product 1");

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be("Product 1");
            response.Model.Quantity.Should().Be(0);
        }

        [Test]
        public void ShoppingListService_GetProducts_ReturnsProducts()
        {
            // When
            var response = CheckoutClient.ShoppingListService.GetProducts();

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model[0].Name.Should().Be("Product 1");
            response.Model[0].Quantity.Should().Be(0);
            response.Model[1].Name.Should().Be("Product 2");
            response.Model[1].Quantity.Should().Be(0);
        }
    }
}
