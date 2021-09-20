
namespace Xero.Shop.Services.Products.ViewModels
{
	public class ProductSearchParameters
	{
		const int maxPageSize = 20;
		public string Name { get; set; }
		public string Description { get; set; }
		public int PageNumber { get; set; } = 1;

		private int _pageSize = 10;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
		}

		public string SortField { get; set; } = "Name";
		public string SortDirection { get; set; } = "asc";
    }
}
