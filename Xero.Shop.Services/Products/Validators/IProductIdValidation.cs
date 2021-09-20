
using System;

namespace Xero.Shop.Services.Products.Validators
{
	public interface IProductIdValidation
	{
		bool IsValid(Guid productId);
	}
}
