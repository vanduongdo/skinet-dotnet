using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Product>> GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);

        var products = await repo.ListAsync(spec);

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        repo.Add(product);

        if (await repo.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); // it give us the created product and the route in the header
        }

        return BadRequest("Product could not be created");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (ProductExists(id)) return BadRequest("Product ID mismatch");

        repo.Update(product);

        if (await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Product could not be updated");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if (product == null) return NotFound();

        repo.Remove(product);
        if (await repo.SaveAllAsync()) return Ok();

        return BadRequest("Product could not be deleted");
    }

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }

    [HttpGet("types")]
    public Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
    {
        throw new NotImplementedException();
    }

    [HttpGet("brands")]
    public Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
    {
        throw new NotImplementedException();
    }
}
