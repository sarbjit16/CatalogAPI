﻿using System;

namespace Xero.Shop.Models.Products
{
	public class Product
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public decimal DeliveryPrice { get; set; }

		//public DateTime CreatedDate { get; set; }
		//public string CreatedBy { get; set; }
		//public DateTime? UpdatedDate { get; set; }
		//public string UpdatedBy { get; set; }

	}
}
