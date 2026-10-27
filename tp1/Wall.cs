namespace TP1;

public enum WallOrientation
{
    Horizontal,
    Vertical,
    Corner
}

public sealed class Wall : Tile
{
    public Wall(WallOrientation orientation = WallOrientation.Horizontal)
    {
        Orientation = orientation;
    }

    public WallOrientation Orientation { get; }

    public override bool IsTraversable => false;

    public override bool Pass(ICollectable? collectable = null) => false;
}
