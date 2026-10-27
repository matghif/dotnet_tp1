using TP1;

namespace TP1.Tests;

public class TileTests
{
    [Fact]
    public void Wall_is_not_traversable()
    {
        var wall = new Wall();

        Assert.False(wall.IsTraversable);
        Assert.False(wall.Pass());
    }

    [Fact]
    public void Room_is_traversable_and_can_contain_a_collectable()
    {
        var key = new Key();
        var roomWithKey = new Room(key);
        var emptyRoom = new Room();

        Assert.True(roomWithKey.IsTraversable);
        Assert.True(roomWithKey.Pass());
        Assert.Same(key, roomWithKey.Collectable);
        Assert.Null(emptyRoom.Collectable);
    }

    [Fact]
    public void Door_starts_closed_and_only_its_key_toggles_it()
    {
        var door = new Door();
        var otherDoor = new Door();

        Assert.False(door.IsTraversable);
        Assert.False(door.Pass());
        Assert.False(door.Pass(otherDoor.Key));
        Assert.False(door.IsTraversable);

        Assert.True(door.Pass(door.Key));
        Assert.True(door.IsTraversable);
        Assert.False(door.Pass(door.Key));
        Assert.False(door.IsTraversable);
    }

    [Fact]
    public void Each_door_creates_a_unique_key()
    {
        var firstDoor = new Door();
        var secondDoor = new Door();

        Assert.NotSame(firstDoor.Key, secondDoor.Key);
        Assert.NotEqual(firstDoor.Key, secondDoor.Key);
        Assert.IsAssignableFrom<ICollectable>(firstDoor.Key);
    }
}
