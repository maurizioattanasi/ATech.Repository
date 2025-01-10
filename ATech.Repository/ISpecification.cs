using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ATech.Repository;

public interface ISpecification<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> Criteria { get; }

    List<Expression<Func<TEntity, object>>> Includes { get; }

    // TODO: Implement OrderBy 

    // TODO: Implement OrderByDescending

    // TODO: Implement Pagination support
}

