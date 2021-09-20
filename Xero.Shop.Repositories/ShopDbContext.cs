using Microsoft.EntityFrameworkCore;
using Xero.Shop.Models.Products;


namespace Xero.Shop.Repositories
{
	public class ShopDbContext : DbContext
	{
		public ShopDbContext(DbContextOptions<ShopDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductOption> ProductOptions { get; set; }
    }
}
