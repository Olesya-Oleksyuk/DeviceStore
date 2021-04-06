using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
  {
    /// <summary>
    /// Connect Criteria-expression and Includes with DbSet to get actual Query to DB.
    /// </summary>
    /// <param name="inputQuery">DbSet of type "EntityName". Thanks to DbSet type, we can use DBSet context methods 
    /// such as Where() to actually apply the queires form spec.Criteria into our expression.</param>
    /// <param name="spec">The spec, which contains Criteria Linq-Expression and Include statements.</param>
    /// <returns>IQueryable query-object to call an async DB method on.</returns>
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
      var query = inputQuery;

      // Evaluate what's inside this specification
      if (spec.Criteria != null)
      {
        // Give me a Product, where the Product matches the Criteria from the Spec (i.e. matches given lambda expression)
        query = query.Where(spec.Criteria); // Criteria = (p => p.ProductTypeId == id) - lambda expression
      }

      // Evalute the Includes:
      // current - is the entity that we're passing in here
      // include - is the expression of our "Includes" statment. 
      query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

      return query;

    }

  }
}