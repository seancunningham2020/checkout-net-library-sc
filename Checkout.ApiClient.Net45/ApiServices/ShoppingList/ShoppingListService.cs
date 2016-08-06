using Checkout.ApiServices.SharedModels;
using System.Collections.Generic;

namespace Checkout.ApiServices.ShoppingList
{
    public class ShoppingListService
    {
        public HttpResponse<Product> GetProduct(string name)
        {
            var getProductUri = string.Format(ApiUrls.GetShoppingListProduct, name);
            return new ApiHttpClient().GetRequest<Product>(getProductUri, AppSettings.SecretKey);
        }

        public HttpResponse<List<Product>> GetProducts()
        {
            var getProductsUri = string.Format(ApiUrls.GetShoppingListProducts);
            return new ApiHttpClient().GetRequest<List<Product>>(getProductsUri, AppSettings.SecretKey);
        }
    }
}
