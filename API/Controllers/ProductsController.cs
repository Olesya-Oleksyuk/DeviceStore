using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
      var products = await _repo.GetProductsAsync();
      return Ok(products);
    }

    [HttpGet("{id}")] // get "id" as a route paramater 
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      return await _repo.GetProductByIdAsync(id);
    }


    /// <summary>
    /// Returns the list of Brands
    /// </summary>
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _repo.GetProductBrandAsync());
    }

    /// <summary>
    /// Returns the list of Types
    /// </summary>
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
      return Ok(await _repo.GetProductTypeAsync());
    }


  }
}

