using System;
using System.Linq;
using Xero.Shop.Repositories.Products;

namespace Xero.Shop.Services.Products.Validators
{
	public class ProductIdValidation : IProductIdValidation
	{
		private readonly IProductRepository _productRepository;

		public ProductIdValidation(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public bool IsValid(Guid productId)
		{
			return _productRepository.GetEntities().Any(x => x.Id == productId);
		}
	}
}
