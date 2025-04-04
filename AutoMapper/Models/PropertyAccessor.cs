namespace AutoMapper.Models;

public record PropertyAccessor()
{
    public Func<object, object?> Getter { get; init; } = null!;
    public Action<object, object?> Setter { get; init; } = null!;
    public Type PropertyType { get; init; } = null!;
}