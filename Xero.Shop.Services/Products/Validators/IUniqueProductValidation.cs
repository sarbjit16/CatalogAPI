
using System;

namespace Xero.Shop.Services.Products.Validators
{
	public interface IUniqueProductValidation
	{
		bool IsValid(string name, Guid? productId = null);
	}
}
