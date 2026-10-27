namespace TP1;

public sealed class Room : Tile
{
    public Room(ICollectable? collectable = null)
    {
        Collectable = collectable;
    }

    public ICollectable? Collectable { get; }

    public override bool IsTraversable => true;

    public override bool Pass(ICollectable? collectable = null) => true;
}
