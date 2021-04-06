namespace Core.Entities
{
  public class Product : BaseEntity
  {
    // id comes implicitly by BaseEntity
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }

    // Related Entities
    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }
    public ProductBrand ProductBrand { get; set; }
    public int ProductBrandId { get; set; }
  }
}