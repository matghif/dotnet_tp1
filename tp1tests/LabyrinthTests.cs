using TP1;

namespace TP1.Tests;

public class LabyrinthTests
{
    [Fact]
    public void Dimensions_are_taken_from_the_first_row_and_number_of_rows()
    {
        var labyrinth = new Labyrinth("+--+|\n|/k |\n+---+");

        Assert.Equal(5, labyrinth.Width);
        Assert.Equal(3, labyrinth.Height);
        Assert.Equal(5, labyrinth.Tiles.GetLength(0));
        Assert.Equal(3, labyrinth.Tiles.GetLength(1));
    }

    [Fact]
    public void Parses_wall_orientations_rooms_doors_and_keys()
    {
        var labyrinth = new Labyrinth("+--+|\n|/k |\n+---+");

        Assert.Equal(WallOrientation.Corner, Assert.IsType<Wall>(labyrinth.Tiles[0, 0]).Orientation);
        Assert.Equal(WallOrientation.Horizontal, Assert.IsType<Wall>(labyrinth.Tiles[1, 0]).Orientation);
        Assert.Equal(WallOrientation.Vertical, Assert.IsType<Wall>(labyrinth.Tiles[4, 0]).Orientation);
        Assert.IsType<Door>(labyrinth.Tiles[1, 1]);
        Assert.IsType<Room>(labyrinth.Tiles[2, 1]);
        Assert.IsType<Room>(labyrinth.Tiles[3, 1]);
        Assert.IsType<Key>(Assert.IsType<Room>(labyrinth.Tiles[2, 1]).Collectable);
        Assert.Null(Assert.IsType<Room>(labyrinth.Tiles[3, 1]).Collectable);
    }

    [Fact]
    public void Pairs_keys_with_doors_in_reading_order()
    {
        var labyrinth = new Labyrinth("/k/k");
        var firstDoor = Assert.IsType<Door>(labyrinth.Tiles[0, 0]);
        var secondDoor = Assert.IsType<Door>(labyrinth.Tiles[2, 0]);
        var firstKeyRoom = Assert.IsType<Room>(labyrinth.Tiles[1, 0]);
        var secondKeyRoom = Assert.IsType<Room>(labyrinth.Tiles[3, 0]);

        Assert.Same(firstDoor.Key, firstKeyRoom.Collectable);
        Assert.Same(secondDoor.Key, secondKeyRoom.Collectable);
        Assert.Same(firstDoor, labyrinth.DoorKeys[firstDoor.Key]);
        Assert.Same(secondDoor, labyrinth.DoorKeys[secondDoor.Key]);
    }

    [Fact]
    public void Short_rows_are_padded_with_empty_rooms()
    {
        var labyrinth = new Labyrinth("+--+\n|\n+--+");

        Assert.Equal(4, labyrinth.Width);
        Assert.IsType<Room>(labyrinth.Tiles[1, 1]);
        Assert.IsType<Room>(labyrinth.Tiles[3, 1]);
    }

    [Fact]
    public void Windows_line_endings_are_supported()
    {
        var labyrinth = new Labyrinth("+--+\r\n|k/|\r\n+--+");

        Assert.Equal(4, labyrinth.Width);
        Assert.Equal(3, labyrinth.Height);
        Assert.Single(labyrinth.DoorKeys);
    }

    [Fact]
    public void Rejects_empty_layouts_unsupported_characters_and_unmatched_keys_or_doors()
    {
        Assert.Throws<ArgumentException>(() => new Labyrinth(""));
        Assert.Throws<ArgumentException>(() => new Labyrinth("\n--"));
        Assert.Throws<ArgumentException>(() => new Labyrinth("+--+\n+---+"));
        Assert.Throws<ArgumentException>(() => new Labyrinth("+x+"));
        Assert.Throws<ArgumentException>(() => new Labyrinth("/  "));
    }

    [Fact]
    public void Render_preserves_the_layout_symbols_and_dimensions()
    {
        var layout = "+--+\n|/k|\n+--+";
        var labyrinth = new Labyrinth(layout);

        Assert.Equal(layout, labyrinth.Render());
    }
}
