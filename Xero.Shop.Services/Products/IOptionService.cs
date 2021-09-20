using System;
using System.Collections.Generic;
using Xero.Shop.Services.Products.ViewModels.ProductOptions;

namespace Xero.Shop.Services.Products
{
	public interface IOptionService
	{
		ProductOptionView GetOption(Guid productId, Guid optionId);
		IEnumerable<ProductOptionView> GetOptions(Guid productId);
		void DeleteOption(Guid productId, Guid optionId);
		void AddOption(CreateProductOptionRequest createProductOptionRequest);
		void UpdateOption(UpdateProductOptionRequest vm);
		bool IdempotentCheck(Guid clientId);
	}
}
