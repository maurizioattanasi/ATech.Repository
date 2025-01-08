namespace ATech.Repository;

public interface ISpecification<TEntity>
{
}

public interface ISpecification<TEntity, TResult> : ISpecification<TEntity>
{
}
