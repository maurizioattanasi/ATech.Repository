using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace ATech.Repository.EntityFrameworkCore;

public class SpecificationEvaluator<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecification<TEntity> specification)
    {
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy is not null)
        {
            query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDescending is not null)
        {
            query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.Skip.HasValue)
        {
            query.Skip(specification.Skip.Value);
        }

        if (specification.Take.HasValue)
        {
            query.Take(specification.Take.Value);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}

