using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
  public interface ISpecification<T>
  {
    // What's the criteria of the thing that we're gonna get (similar to the Where() expression)
    Expression<Func<T, bool>> Criteria { get; }
    // For "includes" operation (navigation properties)
    List<Expression<Func<T, object>>> Includes { get; }

    // 2 additional expressions (expressions of a generic variety, pass Func, which is gonna take a type T & return 'object' )
    // 2 выражения обобщенного типа 
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
  }
}