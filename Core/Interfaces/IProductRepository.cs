using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IProductRepository
  {


    /// <summary>
    /// Return an individual Product
    /// </summary>
    Task<Product> GetProductByIdAsync(int id);

    /// <summary>
    /// Return a read-only Collection of Products that can be accessed by index.
    /// </summary>
    Task<IReadOnlyList<Product>> GetProductsAsync();
    /// <summary>
    /// Return a read-only Collection of Product Brands.
    /// </summary>
    Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
    /// <summary>
    /// Return a read-only Collection of Product Types.
    /// </summary>
    Task<IReadOnlyList<ProductType>> GetProductTypeAsync();

  }
}