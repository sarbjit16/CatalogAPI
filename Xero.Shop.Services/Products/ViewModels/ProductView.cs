using System;

namespace Xero.Shop.Services.Products.ViewModels
{
	public class ProductView
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public decimal DeliveryPrice { get; set; }
	}
}
