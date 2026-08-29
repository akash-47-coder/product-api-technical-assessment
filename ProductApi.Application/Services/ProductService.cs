using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProductApi.Application.DTOs;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return null;

        return Map(product);
    }

    public async Task<(IEnumerable<ProductDto> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize)
    {
        var result = await _repository.GetPagedAsync(
            pageNumber,
            pageSize);

        var products = result.Items
            .Select(Map)
            .ToList();

        return (products, result.TotalCount);
    }

    public async Task<ProductDto> CreateAsync(
        CreateProductRequest request,
        string createdBy)
    {
        var product = new Product
        {
            ProductName = request.ProductName,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };

        await _repository.AddAsync(product);

        return Map(product);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateProductRequest request,
        string modifiedBy)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        product.ProductName = request.ProductName;
        product.ModifiedBy = modifiedBy;
        product.ModifiedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        await _repository.DeleteAsync(product);

        return true;
    }

    private static ProductDto Map(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            CreatedBy = product.CreatedBy,
            CreatedOn = product.CreatedOn,
            ModifiedBy = product.ModifiedBy,
            ModifiedOn = product.ModifiedOn
        };
    }
}
