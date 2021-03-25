using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data
{
  public class StoreContext : DbContext
  {
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    // This property allows us to query those entities and retrieve the data we're looking for from DB
    public DbSet<Product> Products { get; set; }
  }
}