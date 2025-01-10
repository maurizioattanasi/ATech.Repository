using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ATech.Repository;

public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    public Specification()
    {
    }

    public Specification(int? skip = null, int? take = null)
    {
        Skip = skip;
        Take = take;
    }

    public Specification(Expression<Func<TEntity, bool>> criteria)
        => Criteria = criteria;

    /// <inheritdoc/>
    public Expression<Func<TEntity, bool>> Criteria { get; init; } = null!;

    /// <inheritdoc/>
    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

    /// <inheritdoc/>
    public Expression<Func<TEntity, object>> OrderBy { get; private set; } = null!;

    /// <inheritdoc/>
    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; } = null!;

    /// <inheritdoc/>
    public int? Take { get; private set; }

    /// <inheritdoc/>
    public int? Skip { get; private set; }

    /// <inheritdoc/>
    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        => Includes.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderby)
        => OrderBy = orderby;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderbyDescending)
        => OrderByDescending = orderbyDescending;
}

