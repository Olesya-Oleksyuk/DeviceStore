using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IProductRepository
  {

    Task<Product> GetProductByIdAsync(int id);

    /// <summary>
    /// Represents a read-only collection of elements that can be accessed by index.
    /// </summary>
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
    Task<IReadOnlyList<ProductType>> GetProductTypeAsync();

  }
}