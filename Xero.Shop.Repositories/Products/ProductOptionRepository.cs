using Xero.Shop.Models.Products;

namespace Xero.Shop.Repositories.Products
{
	public class ProductOptionRepository : BaseRepository<ProductOption>, IProductOptionRepository
	{
		public ProductOptionRepository(ShopDbContext dataContext) : base(dataContext)
		{

		}
	}
}
