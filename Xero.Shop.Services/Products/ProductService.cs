using System;
using System.Linq;
using Ganss.XSS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xero.Shop.Infrastructure.Exceptions;
using Xero.Shop.Infrastructure.Paging;
using Xero.Shop.Repositories.Products;
using Xero.Shop.Services.Products.Mapper;
using Xero.Shop.Services.Products.ViewModels;

namespace Xero.Shop.Services.Products
{
	public class ProductService: IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IProductOptionRepository _productOptionRepository;
		private readonly ILogger<ProductService> _logger;

		public ProductService(IProductRepository productRepository, IProductOptionRepository productOptionRepository, ILogger<ProductService> logger)
		{
			_productRepository = productRepository;
			_productOptionRepository = productOptionRepository;
			_logger = logger;
		}

		public ProductView GetProduct(Guid productId)
		{
			try
			{
				var product = _productRepository.GetEntities()
					.Where(x => x.Id == productId)
					.Select(x =>
						new ProductView
						{
							Id = x.Id,
							Name = x.Name,
							Description = x.Description,
							Price = x.Price,
							DeliveryPrice = x.DeliveryPrice
						}).SingleOrDefault();

				if (product == null)
				{
					throw new NotFoundException("Product", productId);
				}

				return product;
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"GetProduct failed, productId '{productId}'");
				throw;
			}
		}

		public PagedList<ProductView> GetProducts(ProductSearchParameters productSearchParameters)
		{
			try
			{
				var query = _productRepository.GetEntities()
					.Select(x => new ProductView
					{
						Id = x.Id,
						Name = x.Name,
						Description = x.Description,
						Price = x.Price,
						DeliveryPrice = x.DeliveryPrice
					});

				if (!string.IsNullOrWhiteSpace(productSearchParameters.Name))
				{
					productSearchParameters.Name = productSearchParameters.Name.Trim();
					query = query.Where(x => EF.Functions.Like(x.Name, "%"+ productSearchParameters.Name + "%"));
				}

				if (!string.IsNullOrWhiteSpace(productSearchParameters.Description))
				{
					productSearchParameters.Description = productSearchParameters.Description.Trim();
					query = query.Where(x => EF.Functions.Like(x.Description, "%" + productSearchParameters.Description + "%"));
				}

				////TODO add filters for other columns;
				////TODO extend filter using generic helpers

				var sortField = productSearchParameters.SortField;
				if (!string.IsNullOrWhiteSpace(productSearchParameters.SortField))
				{
					sortField = productSearchParameters.SortField;
				}

				if (productSearchParameters.SortDirection == "desc")
				{
					query = query.OrderByDescending(sortField);
				}
				else
				{
					query = query.OrderBy(sortField);
				}

				return PagedList<ProductView>.ToPagedList(query, productSearchParameters.PageNumber, productSearchParameters.PageSize);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"GetProducts failed");
				throw;
			}
		}

		public void AddProduct(CreateProductRequest createProductRequest)
		{
			try
			{
				var product = createProductRequest.Map();

				var sanitizer = new HtmlSanitizer();
				
				product.Description = sanitizer.Sanitize(product.Description);

				_productRepository.AddAsync(product);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"AddProduct failed, ClientId '{createProductRequest.ClientId}'");
				throw;
			}
		}

		public void UpdateProduct(UpdateProductRequest updateProductRequest)
		{
			try
			{
				////TODO implement concurrency

				var product = _productRepository
					.GetEntities()
					.Single(x => x.Id == updateProductRequest.Id);

				if (product == null)
				{
					throw new NotFoundException("Product", updateProductRequest.Id);
				}

				updateProductRequest.Map(product);

				////TODO audit logs
				_productRepository.Update(product);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"UpdateProduct failed, ProductId '{updateProductRequest.Id}'");
				throw;
			}
		}

		public void  DeleteProduct(Guid productId)
		{
			try
			{
				var product = _productRepository.Get(productId);

				if (product == null)
				{
					throw new NotFoundException("Product", productId);
				}

				
				_productRepository.BeginTransaction();

				try
				{
					var productOptions = _productOptionRepository.GetEntities().Where(x => x.ProductId == productId);

					foreach (var productOption in productOptions)
					{
						_productOptionRepository.Delete(productOption);
					}

					_productRepository.Delete(product);

					////TODO audit logs
				
					_productRepository.Commit();
				}
				catch (Exception)
				{
					_productRepository.RollBack();
					throw;
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"DeleteProduct failed, productId '{productId}'");

				throw;
			}
		}

		public bool IdempotentCheck(Guid clientId)
		{
			return _productRepository.GetEntities().Any(x => x.Id == clientId);
		}
	}
}
