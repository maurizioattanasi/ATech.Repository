using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace ATech.Repository;

public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    private readonly List<Expression<Func<TEntity, object>>> _includes = new();

    protected Specification()
        => Includes = new ReadOnlyCollection<Expression<Func<TEntity, object>>>(_includes);

    protected Specification(int? skip, int? take) : this()
    {
        Skip = skip;
        Take = take;
    }

    protected Specification(Expression<Func<TEntity, bool>> criteria) : this()
        => Criteria = criteria;

    public ReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; internal set; }
    /// <inheritdoc/>
    public Expression<Func<TEntity, bool>> Criteria { get; init; } = null!;

    /// <inheritdoc/>

    /// <inheritdoc/>
    public Expression<Func<TEntity, object>> OrderBy { get; private set; } = null!;

    /// <inheritdoc/>
    public Expression<Func<TEntity, object>> OrderByDescending { get; private set; } = null!;

    /// <inheritdoc/>
    public int? Take { get; private set; }

    /// <inheritdoc/>
    public int? Skip { get; private set; }

    /// <inheritdoc/>
    public bool AsNoTracking { get; private set; }

    /// <inheritdoc/>
    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        => _includes.Add(includeExpression);

    /// <inheritdoc/>
    protected void AddOrderBy(Expression<Func<TEntity, object>> orderby)
        => OrderBy = orderby;

    /// <inheritdoc/>
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderbyDescending)
        => OrderByDescending = orderbyDescending;

    /// <inheritdoc/>
    protected void ApplyNoTracking()
        => AsNoTracking = true;
}

