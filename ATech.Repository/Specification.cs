using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ATech.Repository;

public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    public Specification(Expression<Func<TEntity, bool>> criteria)
        => Criteria = criteria;

    public Expression<Func<TEntity, bool>> Criteria { get; init; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) 
        => Includes.Add(includeExpression);
}

