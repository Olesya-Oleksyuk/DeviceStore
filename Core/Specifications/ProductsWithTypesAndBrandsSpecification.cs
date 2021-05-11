using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrandsSpecification(string sort)
    {
      // Add required "Include()" statements to the Includes list. 
      AddInclude(x => x.ProductType);
      AddInclude(x => x.ProductBrand);
      // ordering by name (по умолч: возвращаем продукты в алфавитном порядке - вариант без указания парамтера sort)
      AddOrderBy(x => x.Name);

      // apply required sorting based on 'sort' parameter
      if (!string.IsNullOrEmpty(sort))
      {
        switch (sort)
        {
          case "priceAsc":
            AddOrderBy(p => p.Price);
            break;
          case "priceDesc":
            AddOrderByDescending(p => p.Price);
            break;
          default:
            AddOrderBy(n => n.Name); break;
        }
      }
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
      AddInclude(x => x.ProductType);
      AddInclude(x => x.ProductBrand);
    }
  }
}