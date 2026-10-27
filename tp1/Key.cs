namespace TP1;

public sealed record Key : ICollectable
{
    public Guid Id { get; } = Guid.NewGuid();
}
