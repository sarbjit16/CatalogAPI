using System;
using System.ComponentModel.DataAnnotations;

namespace Xero.Shop.Infrastructure.Validations.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class IsGuidNotEmptyAttribute : ValidationAttribute
	{
		public const string DefaultErrorMessage = "The {0} field can not be empty";
		public IsGuidNotEmptyAttribute() : base(DefaultErrorMessage) { }

		public override bool IsValid(object value)
		{
			if (value is Guid)
			{
				return Guid.Parse(value.ToString()) != Guid.Empty;
			}

			return true;
		}
    }
}
