using System;
using DotNext;

namespace Delve; 

public class Tile {
    private readonly Map map;
    public readonly int X;
    public readonly uint Y;
    public bool Valid {
        get {
            var tile = map.GetTile(X, Y);
            return tile ? tile.Value == this : false;
        }
    }

    public bool Empty => Room == null;

    public Connectors Connectors;
    public Room? Room;

    public Tile(Map map, int x, uint y) {
        this.map = map;
        if (X < map.LeftBound || X > map.RightBound)
            throw new ArgumentOutOfRangeException(nameof(x));
        X = x;
        if (Y > map.BottomBound)
            throw new ArgumentOutOfRangeException(nameof(y));
        Y = y;
        Connectors = new Connectors();
    }

    public bool Connect(Tile other) {
        var check = CheckConnectable(other);
        if (!check.IsSuccessful) return false;
        switch (check.Value) {
            case Direction.Right:
                if (Connectors.Right && other.Connectors.Left)
                    return false;
                Connectors.Right = true;
                other.Connectors.Left = true;
                break;
            case Direction.Up:
                if (Connectors.Up && other.Connectors.Down)
                    return false;
                Connectors.Up = true;
                other.Connectors.Down = true;
                break;
            case Direction.Left:
                if (Connectors.Left && other.Connectors.Right)
                    return false;
                Connectors.Left = true;
                other.Connectors.Right = true;
                break;
            case Direction.Down:
                if (Connectors.Down && other.Connectors.Up)
                    return false;
                Connectors.Down = true;
                other.Connectors.Up = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(other));
        }
        return true;
    }

    public Result<Direction> CheckConnectable(Tile other) {
        if (this == other)
            return new Result<Direction>(new ArgumentException(null, nameof(other)));
        if (!Valid || !other.Valid)
            return new Result<Direction>(new InvalidOperationException());
        if (Room is null || other.Room is null)
            return new Result<Direction>(new InvalidOperationException());
        var sameX = X == other.X;
        var sameY = Y == other.Y;
        if ((sameX && sameY) || (!sameX && !sameY))
            return new Result<Direction>(new InvalidOperationException());
        if (sameY) {
            if (X - 1 == other.X)
                return Direction.Left;
            if (X + 1 == other.X)
                return Direction.Right;
        }
        if (sameX) {
            if (Y - 1 == other.Y)
                return Direction.Up;
            if (Y + 1 == other.Y)
                return Direction.Down;
        }
        return new Result<Direction>(new InvalidOperationException());
    }
}