using System;

namespace Xero.Shop.Infrastructure.Exceptions
{
	public class BadRequestException : ApplicationException
	{
		public BadRequestException(string message) : base(message)
		{

		}
	}
}