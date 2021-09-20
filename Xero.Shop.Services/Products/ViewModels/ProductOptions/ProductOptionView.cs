using System;

namespace Xero.Shop.Services.Products.ViewModels.ProductOptions
{
	public  class ProductOptionView
	{
		public Guid Id { get; set; }

		public Guid ProductId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
