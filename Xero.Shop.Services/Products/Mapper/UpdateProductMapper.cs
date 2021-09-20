using Xero.Shop.Models.Products;
using Xero.Shop.Services.Products.ViewModels;

namespace Xero.Shop.Services.Products.Mapper
{
	public static class UpdateProductMapper
	{
		public static void Map(this UpdateProductRequest vm, Product product)
		{
			product.Name = vm.Name;
			product.Description = vm.Description;
			product.Price = vm.Price.GetValueOrDefault();
			product.DeliveryPrice = vm.DeliveryPrice.GetValueOrDefault();
		}
	}
}
