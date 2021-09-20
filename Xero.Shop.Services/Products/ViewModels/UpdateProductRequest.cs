using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ganss.XSS;
using Microsoft.Extensions.DependencyInjection;
using Xero.Shop.Infrastructure.Validations.Attributes;
using Xero.Shop.Services.Products.Validators;

namespace Xero.Shop.Services.Products.ViewModels
{
	public class UpdateProductRequest : IValidatableObject
	{
		[IsGuidNotEmpty]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Product name is required")]
		[MaxLength(200, ErrorMessage = "Product name  can not be greater than 200")]
		public string Name { get; set; }

		[Required]
		[MaxLength(2000, ErrorMessage = "Description can not be greater than 2000")]
		public string Description { get; set; }

		[Required]
		[Range(1,9999)]
		public decimal? Price { get; set; }

		[Required]
		public decimal? DeliveryPrice { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var errors = new List<ValidationResult>();
				
			var uniqueProductValidation = validationContext.GetService<IUniqueProductValidation>();

			if (!uniqueProductValidation.IsValid(Name, Id))
			{
				errors.Add(new ValidationResult("Product name already exist", new[] { nameof(Name) }));
			}

			return errors;
		}
	}
}
