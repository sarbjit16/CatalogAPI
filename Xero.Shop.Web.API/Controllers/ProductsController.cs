using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xero.Shop.Services.Products;
using Xero.Shop.Services.Products.ViewModels;

namespace Xero.Shop.Web.API.Controllers
{
	[ApiController]
	[Route("api/products")]
	[ApiConventionType(typeof(DefaultApiConventions))]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly ILogger<ProductsController> _logger;
		 
		public ProductsController(IProductService productService, ILogger<ProductsController> logger)
		{
			_productService = productService;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult GetProducts([FromQuery] ProductSearchParameters productSearchParameters)
		{
			_logger.LogDebug("GetProducts called");

			var products = _productService.GetProducts(productSearchParameters);

			var pagingData = new
			{
				products.TotalCount,
				products.PageSize,
				products.CurrentPage,
				products.TotalPages,
				products.HasNext,
				products.HasPrevious
			};

			Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagingData));

			return Ok(products);
		}

		[HttpGet("{productId}", Name = "GetProduct")]
		public IActionResult GetProduct(Guid productId)
		{
			_logger.LogDebug($"GetProduct called, productId : {productId}");

			var product = _productService.GetProduct(productId);

			return Ok(product);
		}

		[HttpPost]
		public IActionResult CreateProduct([FromQuery] CreateProductRequest createProductRequest)
		{
			_logger.LogDebug($"GetProduct called, ClientId : {createProductRequest.ClientId}");

			if (_productService.IdempotentCheck(createProductRequest.ClientId))
			{
				UpdateProduct(new UpdateProductRequest
				{
					Id = createProductRequest.ClientId,
					Name = createProductRequest.Name,
					Description = createProductRequest.Description,
					Price = createProductRequest.Price,
					DeliveryPrice = createProductRequest.DeliveryPrice
				});
			}
			else
			{
				_productService.AddProduct(createProductRequest);
			}

			return CreatedAtRoute("GetProduct", new { productId = createProductRequest.ClientId }, createProductRequest);
		}

		[HttpPut]
		public IActionResult UpdateProduct([FromQuery] UpdateProductRequest updateProductRequest)
		{
			_logger.LogDebug($"UpdateProduct called, Id : {updateProductRequest.Id}");

			_productService.UpdateProduct(updateProductRequest);

			return CreatedAtRoute("GetProduct", new { productId = updateProductRequest.Id }, updateProductRequest);
		}

		[HttpDelete("{productId}")]
		public IActionResult DeleteProduct(Guid productId)
		{
			_logger.LogDebug($"DeleteProduct called, productId : {productId}");

			_productService.DeleteProduct(productId);

			return NoContent();
		}

		[HttpOptions]
		public IActionResult GetProductsOptions()
		{
			Response.Headers.Add("Allow", "GET,OPTIONS,PUT,POST,DELETE");
			return Ok();
		}
	}
}