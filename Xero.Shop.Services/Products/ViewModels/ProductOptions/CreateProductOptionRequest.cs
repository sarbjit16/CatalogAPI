using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Xero.Shop.Infrastructure.Validations.Attributes;
using Xero.Shop.Services.Products.Validators;

namespace Xero.Shop.Services.Products.ViewModels.ProductOptions
{
	public class CreateProductOptionRequest : IValidatableObject
	{
		[Required]
		[IsGuidNotEmpty]
		public Guid ClientId { get; set; }

		[Required(ErrorMessage = "Product option name is required")]
		[MaxLength(200, ErrorMessage = "Product option name can not be greater than 200")]
		public string Name { get; set; }

		[Required]
		[MaxLength(2000, ErrorMessage = "Description can not be greater than 2000")]
		public string Description { get; set; }

		[Required]
		[IsGuidNotEmpty]
		public Guid ProductId { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var errors = new List<ValidationResult>();
				
			var productIdValidation = validationContext.GetService<IProductIdValidation>();

			if (!productIdValidation.IsValid(ProductId))
			{
				errors.Add(new ValidationResult("Invalid ProductId", new[] { nameof(Name) }));
			}

			return errors;
		}
	}
}
