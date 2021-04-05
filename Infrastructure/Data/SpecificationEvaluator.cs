using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
  {
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
      var query = inputQuery;

      // Evaluate what's inside this specification
      if (spec.Criteria != null)
      {
        query = query.Where(spec.Criteria);
      }

      // Evalute the Includes:
      // current - is the entity that we're passing in here
      // include - is the expression of our "Includes" statment. 
      query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

      return query;

    }

  }
}