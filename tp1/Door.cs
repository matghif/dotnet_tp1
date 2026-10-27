namespace TP1;

public sealed class Door : Tile
{
    private bool _isOpen;

    public Door()
    {
        Key = new Key();
    }

    public Key Key { get; }

    public override bool IsTraversable => _isOpen;

    public override bool Pass(ICollectable? collectable = null)
    {
        if (ReferenceEquals(collectable, Key))
        {
            _isOpen = !_isOpen;
        }

        return IsTraversable;
    }
}
