using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xero.Shop.Infrastructure.Validations.Attributes;

namespace Xero.Shop.Services.Products.ViewModels.ProductOptions
{
	public class UpdateProductOptionRequest : IValidatableObject
	{
		[Required]
		[IsGuidNotEmpty]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Product option name is required")]
		[MaxLength(200, ErrorMessage = "Product option name can not be greater than 200")]
		public string Name { get; set; }

		[Required]
		[MaxLength(2000, ErrorMessage = "Description can not be greater than 2000")]
		public string Description { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var errors = new List<ValidationResult>();

			////TODO Unique name check

			return errors;
		}
	}
}
