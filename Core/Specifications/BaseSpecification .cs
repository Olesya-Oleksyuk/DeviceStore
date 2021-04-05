using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
  public class BaseSpecification<T> : ISpecification<T>
  {
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
      Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }



    /// <summary>
    /// The list of Include() statements. 
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } =
        new List<Expression<Func<T, object>>>();



    /// <summary>
    ///  Allows us to add Include() statements to "Includes" list.
    /// </summary>
    /// <param name="includeExpression"></param>
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
      Includes.Add(includeExpression);
    }


  }
}