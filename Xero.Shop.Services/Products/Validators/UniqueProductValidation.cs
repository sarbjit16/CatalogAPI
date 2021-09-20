using System;
using System.Linq;
using Xero.Shop.Repositories.Products;

namespace Xero.Shop.Services.Products.Validators
{
	public class UniqueProductValidation : IUniqueProductValidation
	{
		private readonly IProductRepository _productRepository;

		public UniqueProductValidation(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public bool IsValid(string name, Guid? productId= null)
		{
			var  query = _productRepository.GetEntities().AsQueryable();
			
			if (productId.HasValue)
			{
				query= query.Where(x => x.Id != productId.Value);
			}

			var hasProduct = query.Any(x => x.Name == name);

			return !hasProduct;
		}
	}
}
