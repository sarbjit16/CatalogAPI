using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xero.Shop.Services.Products;
using Xero.Shop.Services.Products.ViewModels.ProductOptions;

namespace Xero.Shop.Web.API.Controllers
{
	[ApiController]
	[Route("api/products/{productId}/options")]
	[ApiConventionType(typeof(DefaultApiConventions))]
	public class ProductsOptionsController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IOptionService _optionService;
		private readonly ILogger<ProductsOptionsController> _logger;

		public ProductsOptionsController(IProductService productService, IOptionService optionService, ILogger<ProductsOptionsController> logger)
		{
			_productService = productService;
			_optionService = optionService;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult GetProductOptions(Guid productId)
		{
			_logger.LogDebug($"GetProductOptions called, productId : {productId}");

			var options = _optionService.GetOptions(productId);

			return Ok(options);
		}

		[HttpGet("{optionId}", Name = "GetProductOption")]
		public IActionResult GetProductOption(Guid productId, Guid optionId)
		{
			_logger.LogDebug($"GetProductOption called, productId : {productId}");

			var productOptions = _optionService.GetOption(productId, optionId);

			return Ok(productOptions);
		}

		[HttpPost]
		public IActionResult CreateOption([FromQuery] CreateProductOptionRequest createProductRequest)
		{
			if (_optionService.IdempotentCheck(createProductRequest.ClientId))
			{
				UpdateProduct(new UpdateProductOptionRequest
				{
					Id = createProductRequest.ClientId,
					Name = createProductRequest.Name,
					Description = createProductRequest.Description
				});
			}
			else
			{
				_optionService.AddOption(createProductRequest);
			}

			return CreatedAtRoute("GetProductOption", new { productId = createProductRequest.ProductId, optionId = createProductRequest.ClientId }, createProductRequest);
		}

		[HttpPut]
		public IActionResult UpdateProduct([FromQuery] UpdateProductOptionRequest updateProductOptionRequest)
		{
			////TODO implement concurrency

			_optionService.UpdateOption(updateProductOptionRequest);

			return CreatedAtRoute("GetProduct", new { productId = updateProductOptionRequest.Id }, updateProductOptionRequest);
		}

		[HttpDelete("{optionId}")]
		public IActionResult DeleteOption(Guid productId, Guid optionId)
		{
			_optionService.DeleteOption(productId, optionId);

			return NoContent();
		}

		[HttpOptions]
		public IActionResult GetProductOptions()
		{
			Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,DELETE");
			return Ok();
		}
	}
}