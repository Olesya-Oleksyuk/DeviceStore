using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;

    public ProductsController(IGenericRepository<Product> productsRepo,
    IGenericRepository<ProductBrand> productBrandRepo,
    IGenericRepository<ProductType> productTypeRepo)
    {
      _productTypeRepo = productTypeRepo;
      _productBrandRepo = productBrandRepo;
      _productsRepo = productsRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
      var spec = new ProductsWithTypesAndBrandsSpecification();
      // return the IReadOnlyList<Product> with navigation properties (it hits the DB)
      var products = await _productsRepo.ListAsync(spec);
      // here we are dealing with the object in the memory (we don't hit the DB now)
      return products.Select(product => new ProductToReturnDto
      {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        PictureUrl = product.PictureUrl,
        Price = product.Price,
        ProductBrand = product.ProductBrand.Name,
        ProductType = product.ProductType.Name
      }).ToList();
    }

    [HttpGet("{id}")] // get "id" as a route paramater 
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(id);
      // return the single Product(id) entity with navigation properties 
      var product = await _productsRepo.GetEntityWithSpec(spec);
      return new ProductToReturnDto
      {
        // map the properties to each other
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        PictureUrl = product.PictureUrl,
        Price = product.Price,
        ProductBrand = product.ProductBrand.Name,
        ProductType = product.ProductType.Name
      };
    }


    /// <summary>
    /// Returns the list of Brands
    /// </summary>
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _productBrandRepo.ListAllAsync());
    }

    /// <summary>
    /// Returns the list of Types
    /// </summary>
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
      return Ok(await _productTypeRepo.ListAllAsync());
    }


  }
}

