using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Xero.Shop.Infrastructure.Exceptions;
using Xero.Shop.Models.Products;
using Xero.Shop.Repositories.Products;
using Xero.Shop.Services.Products.ViewModels.ProductOptions;

namespace Xero.Shop.Services.Products
{
	public class OptionService: IOptionService
	{
		private readonly IProductOptionRepository _productOptionRepository;
		private readonly ILogger<OptionService> _logger;

		public OptionService(IProductOptionRepository productOptionRepository, ILogger<OptionService> logger)
		{
			_productOptionRepository = productOptionRepository;
			_logger = logger;
		}

		public ProductOptionView GetOption(Guid productId, Guid optionId)
		{
			try
			{
				var option = _productOptionRepository.GetEntities()
					.Where(x => x.Id == optionId && x.ProductId == productId)
					.Select(x =>
						new ProductOptionView()
						{
							Id = x.Id,
							Name = x.Name,
							Description = x.Description,
							ProductId = x.ProductId
						}).SingleOrDefault();

				if (option == null)
				{
					throw new NotFoundException("Option", productId + "," + optionId);
				}

				return option;
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Unable to find Product Option, productId : {productId}, optionId : '{optionId}'");
				throw;
			}
		}

		public void DeleteOption(Guid productId, Guid optionId)
		{
			try
			{
				var productOption = _productOptionRepository
					.GetEntities()
					.SingleOrDefault(x => x.Id == optionId && x.ProductId == productId);

				if (productOption==null)
				{
					throw new NotFoundException("Option", productId + ","+ optionId );
				}

				_productOptionRepository.Delete(productOption);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"DeleteOption failed , productId : {productId}, optionId : '{optionId}'");
				throw;
			}
		}

		public IEnumerable<ProductOptionView> GetOptions(Guid productId)
		{
			try
			{
				return _productOptionRepository.GetEntities()
					.Where(x => x.ProductId == productId)
					.Select(x =>
						new ProductOptionView()
						{
							Id = x.Id,
							Name = x.Name,
							Description = x.Description,
							ProductId = x.ProductId
						}).ToList();
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"GetOptions failed , productId : {productId}");

				throw;
			}
		}

		public void AddOption(CreateProductOptionRequest vm)
		{
			try
			{
				var productOption = new ProductOption
				{
					Id = vm.ClientId,
					Name = vm.Name.Trim(),
					Description = vm.Description.Trim(),
					ProductId = vm.ProductId
				};

				_productOptionRepository.AddAsync(productOption);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"AddOption failed , productId : {vm.ClientId}");
				throw;
			}
		}

		public void UpdateOption(UpdateProductOptionRequest vm)
		{
			try
			{
				var productOption = _productOptionRepository.Get(vm.Id);

				productOption.Name = vm.Name.Trim();
				productOption.Description = vm.Description.Trim();

				_productOptionRepository.Update(productOption);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"UpdateOption failed , id : {vm.Id}");
				throw;
			}

		}

		public bool IdempotentCheck(Guid clientId)
		{
			return _productOptionRepository.GetEntities().Any(x => x.Id == clientId);
		}
	}
}
