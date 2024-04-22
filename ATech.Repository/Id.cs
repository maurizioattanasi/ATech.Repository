namespace ATech.Repository;

public class Id<TId>(TId value)
{
    public TId Value { get; } = value;
}
