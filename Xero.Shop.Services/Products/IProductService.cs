using System;
using Xero.Shop.Infrastructure.Paging;
using Xero.Shop.Services.Products.ViewModels;

namespace Xero.Shop.Services.Products
{
	public interface IProductService
	{
		void AddProduct(CreateProductRequest createProductRequest);
		PagedList<ProductView> GetProducts(ProductSearchParameters productSearchParameters);
		ProductView GetProduct(Guid productId);
		void UpdateProduct(UpdateProductRequest updateProductRequest);
		void DeleteProduct(Guid productId);
		bool IdempotentCheck(Guid clientId);
	}
}
