using Checkout.ApiServices.ShoppingList.RequestModels;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Net;

namespace Tests.ShoppingListService
{
    [TestFixture]
    public class ShoppingListServiceTests : BaseServiceTests
    {
        [Test]
        public void ShoppingListService_AddNewProduct_ReturnsProduct()
        {
            // Given
            var productName = string.Format("Added Product {0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            var newProduct = new ProductAdd() { Name = productName, Quantity = 1 };

            // When
            var response = CheckoutClient.ShoppingListService.AddProduct(newProduct);

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be(productName);
            response.Model.Quantity.Should().Be(1);
        }

        [Test]
        public void ShoppingListService_AddExistingProduct_ReturnsError()
        {
            // Given
            var productName = string.Format("Already Added Product {0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            var newProduct = new ProductAdd() { Name = productName, Quantity = 1 };
            var newResponse = CheckoutClient.ShoppingListService.AddProduct(newProduct);
            var repeatProduct = new ProductAdd() { Name = productName, Quantity = 1 };

            // When
            var response = CheckoutClient.ShoppingListService.AddProduct(newProduct);

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.HasError.Should().BeTrue();
            response.Error.Message.Should().Be("This product already exists");
        }
        
        [Test]
        public void ShoppingListService_ProductWithoutName_ReturnsErrorMessage()
        {
            // Given
            var newProduct = new ProductAdd() { Name = string.Empty, Quantity = 1 };

            // When
            var response = CheckoutClient.ShoppingListService.AddProduct(newProduct);

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.HasError.Should().BeTrue();
            response.Error.Message.Should().Be("Product Name is required");
        }

        [Test]
        public void ShoppingListService_ProductWithoutQuantity_ReturnsErrorMessage()
        {
            // Given
            var newProduct = new ProductAdd() { Name = "Added Product", Quantity = 0 };

            // When
            var response = CheckoutClient.ShoppingListService.AddProduct(newProduct);

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.HasError.Should().BeTrue();
            response.Error.Message.Should().Be("Quantity greater than 0 required");
        }

        [Test]
        public void ShoppingListService_GetProduct_ReturnsProduct()
        {
            // Given
            var productName = string.Format("Get Product {0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            var newProduct = new ProductAdd() { Name = productName, Quantity = 1 };
            var addResponse = CheckoutClient.ShoppingListService.AddProduct(newProduct);
            addResponse.Should().NotBeNull();
            addResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            // When
            var response = CheckoutClient.ShoppingListService.GetProduct(productName);

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be(productName);
            response.Model.Quantity.Should().Be(1);
        }

        [Test]
        public void ShoppingListService_GetNotFoundProduct_ReturnsNull()
        {
            // When
            var response = CheckoutClient.ShoppingListService.GetProduct("Not Found Product");

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void ShoppingListService_GetProducts_ReturnsProducts()
        {
            // Given
            var productName = string.Format("Listed Product {0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            var newProduct = new ProductAdd() { Name = productName, Quantity = 1 };
            var addResponse = CheckoutClient.ShoppingListService.AddProduct(newProduct);
            addResponse.Should().NotBeNull();
            addResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            // When
            var response = CheckoutClient.ShoppingListService.GetProducts();

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Find(x => x.Name == productName).Name.Should().Be(productName);
            response.Model.Find(x => x.Name == productName).Quantity.Should().Be(1);
        }

        [Test]
        public void ShoppingListService_DeleteProduct_ReturnsNull()
        {
            // When
            var response = CheckoutClient.ShoppingListService.DeleteProduct("Delete This Product");

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }

        [Test]
        public void ShoppingListService_UpdateProduct_ReturnsOK()
        {
            // Given
            var productName = string.Format("Update Product {0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            var newProduct = new ProductAdd() { Name = productName, Quantity = 1 };
            var addResponse = CheckoutClient.ShoppingListService.AddProduct(newProduct);
            addResponse.Should().NotBeNull();
            addResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            var updatedProduct = new ProductUpdate() { Name = productName, Quantity = 3 };

            // When
            var response = CheckoutClient.ShoppingListService.UpdateProduct(updatedProduct);

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }

        [Test]
        public void ShoppingListSevice_UpdateProductNotFound_ReturnsNotFound()
        {
            // Given
            var updatedProduct = new ProductUpdate() { Name = "No Such Product", Quantity = 3 };

            // When
            var response = CheckoutClient.ShoppingListService.UpdateProduct(updatedProduct);

            // Then
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
