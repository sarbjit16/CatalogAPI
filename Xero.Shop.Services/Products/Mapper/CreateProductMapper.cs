using Xero.Shop.Models.Products;
using Xero.Shop.Services.Products.ViewModels;

namespace Xero.Shop.Services.Products.Mapper
{
	public static class CreateProductMapper
	{
		public static Product Map(this CreateProductRequest vm)
		{
			return new Product
			{
				Id = vm.ClientId,
				Name = vm.Name,
				Description = vm.Description,
				Price = vm.Price.GetValueOrDefault(),
				DeliveryPrice = vm.DeliveryPrice.GetValueOrDefault()
			};
		}
	}
}
