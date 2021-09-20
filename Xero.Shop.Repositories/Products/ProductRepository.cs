using Xero.Shop.Models.Products;

namespace Xero.Shop.Repositories.Products
{
	public class ProductRepository: BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(ShopDbContext dataContext) : base(dataContext)
		{

		}
	}
}
