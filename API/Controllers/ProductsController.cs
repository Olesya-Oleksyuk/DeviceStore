using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class ProductsController : BaseApiController
  {
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;

    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productsRepo,
    IGenericRepository<ProductBrand> productBrandRepo,
    IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
    {
      _mapper = mapper;
      _productTypeRepo = productTypeRepo;
      _productBrandRepo = productBrandRepo;
      _productsRepo = productsRepo;
    }

    // /api/products(?sort=priceAsc)
    //  как зазадётся имя параметра sort - непонятно. 
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(
      string sort, int? brandId, int? typeId)
    {
      // pass "sort" query string to the specification class
      var spec = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);
      // return the IReadOnlyList<Product> with navigation properties (it hits the DB)
      var products = await _productsRepo.ListAsync(spec);
      // here we are dealing with the object in the memory (we don't hit the DB now)
      return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id}")] // get "id" as a route paramater 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(id);
      // return the single Product(id) entity with navigation properties 
      var product = await _productsRepo.GetEntityWithSpec(spec);
      // if the client has requested a product that doesn't exists (i.e. we didn't find a pruduct with a particular ID)
      if (product == null) return NotFound(new ApiResponse(404));
      return _mapper.Map<Product, ProductToReturnDto>(product);
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

