using System;
using System.Xml.Serialization;
using DotNext;
using Godot;
using Delve.Combat;
using System.Collections.Generic;
using Delve.Meta.Rooms;

namespace Delve; 

public class Tile {
    readonly Map map;
    public readonly int X;
    public readonly uint Y;
    bool Valid {
        get {
            var tile = map.GetTile(X, Y);
            return tile ? tile.Value == this : false;
        }
    }

    public bool Empty => Room == null;

    public Connectors Connectors;

    public RoomInstance? Room;
    public List<CombatEntity> CombatEntities;
    

    public Tile(Map map, int x, uint y) {
        this.map = map;
        if (X < Map.LeftTileBound || X > Map.RightTileBound)
            throw new ArgumentOutOfRangeException(nameof(x));
        X = x;
        if (Y > map.BottomTileBound)
            throw new ArgumentOutOfRangeException(nameof(y));
        Y = y;
        Connectors = new Connectors();
        CombatEntities = new List<CombatEntity>();
    }

    public Result Connect(Tile other) {
        var check = CheckConnectable(other);
        if (!check.IsSuccessful) return new Result(check.Error);
        switch (check.Value) {
            case Direction.Right:
                if (Connectors.Right && other.Connectors.Left)
                    return Result.Failure;
                Connectors.Right = true;
                other.Connectors.Left = true;
                break;
            case Direction.Up:
                if (Connectors.Up && other.Connectors.Down)
                    return Result.Failure;
                Connectors.Up = true;
                other.Connectors.Down = true;
                break;
            case Direction.Left:
                if (Connectors.Left && other.Connectors.Right)
                    return Result.Failure;
                Connectors.Left = true;
                other.Connectors.Right = true;
                break;
            case Direction.Down:
                if (Connectors.Down && other.Connectors.Up)
                    return Result.Failure;
                Connectors.Down = true;
                other.Connectors.Up = true;
                break;
            default:
                return Result.FromError(new ArgumentOutOfRangeException(nameof(other)));
        }
        return Result.Success;
    }

    public Result<Direction> CheckConnectable(Tile other) {
        if (this == other)
            return new (new ArgumentException(null, nameof(other)));
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

    public Result<Tile> GetAdjacent(Direction dir) {
        if (!Valid)
            return new Result<Tile>(new InvalidOperationException());
        var (adjacentX, adjacentY) = dir switch {
            Direction.Right => (X + 1, Y),
            Direction.Down => (X, Y + 1),
            Direction.Left => (X - 1, Y),
            Direction.Up => (X, Y - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
        return map.GetTile(adjacentX, adjacentY);
    }
}