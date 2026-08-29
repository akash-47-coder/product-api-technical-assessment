using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ProductApi.API.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1 || pageSize > 100)
            pageSize = 10;

        var result = await _productService.GetPagedAsync(
            pageNumber,
            pageSize);

        return Ok(new
        {
            pageNumber,
            pageSize,
            totalCount = result.TotalCount,
            data = result.Items
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        CreateProductRequest request)
    {
        var createdBy = User.Identity?.Name ?? "system";

        var product = await _productService.CreateAsync(
            request,
            createdBy);

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = product.Id },
            product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(
        int id,
        UpdateProductRequest request)
    {
        var modifiedBy = User.Identity?.Name ?? "system";

        var updated = await _productService.UpdateAsync(
            id,
            request,
            modifiedBy);

        if (!updated)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return NoContent();
    }
}
