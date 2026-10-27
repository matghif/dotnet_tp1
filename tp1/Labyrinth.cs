using System.Collections.ObjectModel;

namespace TP1;

public sealed class Labyrinth
{
    private readonly Dictionary<Key, Door> _doorKeys;

    public Labyrinth(string layout)
    {
        ArgumentNullException.ThrowIfNull(layout); // Sécurité

        var rows = layout.Split('\n')
            .Select(row => row.TrimEnd('\r'))
            .ToArray();

        if (rows[0].Length == 0)
        {
            throw new ArgumentException("The layout must have a non-empty first row.", nameof(layout));
        }

        Width = rows[0].Length;
        Height = rows.Length;

        if (rows.Any(row => row.Length > Width))
        {
            throw new ArgumentException("No layout row can be wider than the first row.", nameof(layout));
        }

        Tiles = new Tile[Width, Height];
        _doorKeys = [];
        var doors = new List<Door>();
        var keyPositions = new List<(int X, int Y)>();

        for (var y = 0; y < Height; y++)
        {
            var row = rows[y].PadRight(Width);

            for (var x = 0; x < Width; x++)
            {
                Tiles[x, y] = row[x] switch
                {
                    '-' => new Wall(WallOrientation.Horizontal),
                    '|' => new Wall(WallOrientation.Vertical),
                    '+' => new Wall(WallOrientation.Corner),
                    '/' => CreateDoor(),
                    'k' => CreateKeyRoom(x, y),
                    ' ' => new Room(),
                    _ => throw new ArgumentException(
                        $"Unsupported layout character '{row[x]}' at position ({x}, {y}).",
                        nameof(layout))
                };
            }
        }

        if (doors.Count != keyPositions.Count)
        {
            throw new ArgumentException("The layout must contain the same number of doors and keys.", nameof(layout));
        }

        for (var index = 0; index < doors.Count; index++)
        {
            var (x, y) = keyPositions[index];
            Tiles[x, y] = new Room(doors[index].Key);
            _doorKeys.Add(doors[index].Key, doors[index]);
        }

        DoorKeys = new ReadOnlyDictionary<Key, Door>(_doorKeys);

        Door CreateDoor()
        {
            var door = new Door();
            doors.Add(door);
            return door;
        }

        Room CreateKeyRoom(int x, int y)
        {
            keyPositions.Add((x, y));
            return new Room();
        }
    }

    public int Width { get; }

    public int Height { get; }

    public Tile[,] Tiles { get; }

    public IReadOnlyDictionary<Key, Door> DoorKeys { get; }

    public string Render()
    {
        var output = new System.Text.StringBuilder();

        for (var y = 0; y < Height; y++)
        {
            if (y > 0)
            {
                output.Append('\n');
            }

            for (var x = 0; x < Width; x++)
            {
                output.Append(Tiles[x, y] switch
                {
                    Wall { Orientation: WallOrientation.Horizontal } => '-',
                    Wall { Orientation: WallOrientation.Vertical } => '|',
                    Wall { Orientation: WallOrientation.Corner } => '+',
                    Door => '/',
                    Room { Collectable: Key } => 'k',
                    Room => ' ',
                    _ => throw new InvalidOperationException(
                        $"Unsupported tile type: {Tiles[x, y].GetType().Name}.")
                });
            }
        }

        return output.ToString();
    }
}