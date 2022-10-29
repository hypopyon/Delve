using System;
using Godot;

namespace Delve; 

public enum Direction {
    Right,
    Up,
    Left,
    Down
}

public static class DirectionExtensions {
    public static Vector2i ToUnitVector(this Direction direction) =>
        direction switch {
            Direction.Right => new Vector2i(1, 0),
            Direction.Up => new Vector2i(0, -1),
            Direction.Left => new Vector2i(-1, 0),
            Direction.Down => new Vector2i(0, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
}