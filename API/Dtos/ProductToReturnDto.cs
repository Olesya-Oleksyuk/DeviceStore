namespace API.Dtos
{
  /// <summary>
  ///  A flat object to return to the client side.
  /// </summary> 
  public class ProductToReturnDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }

    // Related Entities
    public string ProductType { get; set; }
    public string ProductBrand { get; set; }

  }
}