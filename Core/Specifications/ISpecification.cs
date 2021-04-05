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
  }
}