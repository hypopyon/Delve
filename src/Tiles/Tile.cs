using System;
using System.Xml.Schema;
using DotNext;
using Delve.Structures;
using Godot;

namespace Delve.Tiles; 

public class Tile {
    readonly GameMap map;
    public readonly Vector2i Position;
    public int X => Position.x;
    public int Y => Position.y;
    
    bool Valid {
        get {
            var tile = map.GetTile(Position);
            return tile ? tile.Value == this : false;
        }
    }

    public bool Empty => Structure is null;
    public Texture2D Texture => Structure is not null ? Structure.Description.Texture : Textures.Tiles.Unexplored;

    public Tunnels Tunnels;

    public StructureInstance? Structure;
    //public List<CombatEntity> CombatEntities;
    

    public Tile(GameMap map, Vector2i position) {
        this.map = map;
        if (X < GameMap.LeftmostTile || X > GameMap.RightmostTile || Y > map.BottommostTile)
            throw new ArgumentOutOfRangeException(nameof(position));
        Position = position;
        Tunnels = new Tunnels();
        //CombatEntities = new List<CombatEntity>();
    }

    public Result Connect(Tile other) {
        var check = CheckAdjacency(other);
        
        if (check.IsSuccessful == false)
            return new Result(check.Error);
        if (check.Value.HasValue == false)
            return new Result(new InvalidOperationException());
        
        Result ConnectInner(ref bool tunnelA, ref bool tunnelB) {
            if (tunnelA || tunnelB)
                return new Result(new InvalidOperationException());
            tunnelA = true;
            tunnelB = true;
            return Result.Success;
        }

        return check.Value.Value switch {
            Direction.Right => ConnectInner(ref Tunnels.Right, ref other.Tunnels.Left),
            Direction.Up => ConnectInner(ref Tunnels.Up, ref other.Tunnels.Down),
            Direction.Left => ConnectInner(ref Tunnels.Left, ref other.Tunnels.Right),
            Direction.Down => ConnectInner(ref Tunnels.Down, ref other.Tunnels.Up),
            _ => throw new ArgumentOutOfRangeException(nameof(other))
        };
    }
    
    
    public Result<Optional<Direction>> CheckAdjacency(Tile other) {
        if (this == other)
            return new Result<Optional<Direction>>(new ArgumentException(null, nameof(other)));
        if (!Valid || !other.Valid)
            return new Result<Optional<Direction>>(new InvalidOperationException());
        return (X == other.X, Y == other.Y) switch {
            (true, false) when Y - 1 == other.Y => Optional.Some(Direction.Up),
            (true, false) when Y + 1 == other.Y => Optional.Some(Direction.Down),
            (false, true) when X - 1 == other.X => Optional.Some(Direction.Left),
            (false, true) when X + 1 == other.X => Optional.Some(Direction.Right),
            _ => Optional<Direction>.None
        };
    }

    public Result<Tile> GetAdjacent(Direction dir) {
        if (!Valid)
            return new Result<Tile>(new InvalidOperationException());

        var adjacentPosition = Position + dir.ToUnitVector();
        return map.IsPositionInBounds(adjacentPosition)
            ? map.GetTile(adjacentPosition)
            : new Result<Tile>(new ArgumentOutOfRangeException());
    }

    public class CountAdjacentStructuresResult {
        public int Count;
        public Tile? FirstAdjacentStructure;

        public CountAdjacentStructuresResult() {
            Count = 0;
        }
    }

    public CountAdjacentStructuresResult CountAdjacentStructures() {
        var result = new CountAdjacentStructuresResult();

        void CountAdjacent(Direction direction) {
            var getAdjacent = GetAdjacent(direction);
            if (!getAdjacent.IsSuccessful || getAdjacent.Value.Empty) return;
            result.Count++;
            result.FirstAdjacentStructure ??= getAdjacent.Value;
        }
        
        CountAdjacent(Direction.Right);
        CountAdjacent(Direction.Up);
        CountAdjacent(Direction.Left);
        CountAdjacent(Direction.Down);

        return result;
    }
}