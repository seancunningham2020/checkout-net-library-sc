using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.ShoppingList.RequestModels;
using System.Collections.Generic;

namespace Checkout.ApiServices.ShoppingList
{
    public class ShoppingListService
    {
        public HttpResponse<Product> GetProduct(string name)
        {
            var getProductUri = string.Format(ApiUrls.ShoppingListProduct, name);
            return new ApiHttpClient().GetRequest<Product>(getProductUri, AppSettings.SecretKey);
        }

        public HttpResponse<List<Product>> GetProducts()
        {
            var getProductsUri = string.Format(ApiUrls.ShoppingListProducts);
            return new ApiHttpClient().GetRequest<List<Product>>(getProductsUri, AppSettings.SecretKey);
        }

        public HttpResponse<Product> AddProduct(ProductAdd requestModel)
        {
            return new ApiHttpClient().PostRequest<Product>(ApiUrls.ShoppingListProducts, AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> DeleteProduct(string name)
        {
            var deleteProductUri = string.Format(ApiUrls.ShoppingListProduct, name);
            return new ApiHttpClient().DeleteRequest<OkResponse>(deleteProductUri, AppSettings.SecretKey);
        }

        public HttpResponse<OkResponse> UpdateProduct(ProductUpdate requestModel)
        {
            return new ApiHttpClient().PutRequest<OkResponse>(ApiUrls.ShoppingListProduct, AppSettings.SecretKey, requestModel);
        }
    }
}
