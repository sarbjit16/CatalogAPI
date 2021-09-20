﻿using System;

namespace Xero.Shop.Models.Products
{
	public class ProductOption
	{
		public Guid Id { get; set; }

		public Guid ProductId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		//public DateTime CreatedDate { get; set; }
		//public string CreatedBy { get; set; }
		//public DateTime? UpdatedDate { get; set; }
		//public string UpdatedBy { get; set; }
	}
}
