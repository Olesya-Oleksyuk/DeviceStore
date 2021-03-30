using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IProductRepository
  {
    Task<Product> GetProductByIdAsync(int id);
    // Represents a read-only collection of elements that can be accessed by index.
    Task<IReadOnlyList<Product>> GetProductsAsync();
  }
}