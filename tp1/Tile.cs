namespace TP1;

public abstract class Tile
{
    public abstract bool IsTraversable { get; }

    public abstract bool Pass(ICollectable? collectable = null);
}
