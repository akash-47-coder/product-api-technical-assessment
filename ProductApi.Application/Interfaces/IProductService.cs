using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProductApi.Application.DTOs;

namespace ProductApi.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto?> GetByIdAsync(int id);

    Task<(IEnumerable<ProductDto> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize);

    Task<ProductDto> CreateAsync(
        CreateProductRequest request,
        string createdBy);

    Task<bool> UpdateAsync(
        int id,
        UpdateProductRequest request,
        string modifiedBy);

    Task<bool> DeleteAsync(int id);
}
