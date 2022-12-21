

using System;
using DotNext;
using System.Collections.Generic;
using System.Linq;
using Delve.Tiles;
using Godot;

namespace Delve.Structures; 

public class StructurePlacementBuffer {
    GameMap map;
    List<Vector2i> positions;
    int lowestY;
    bool finished;
    
    public StructurePlacementBuffer(GameMap map) {
        this.map = map;
        positions = new List<Vector2i>();
        lowestY = 0;
        finished = false;
    }
    
    public Result Add(Vector2i position) {
        var getTile = map.GetTile(position);
        if (!getTile.IsSuccessful || !getTile.Value.Empty) return new Result(new ArgumentException());
        positions.Add(position);
        if (position.y > lowestY)
            lowestY = position.y;
        return Result.Success;
    }
    
    bool IsContiguousAndValid() {
        var remainingTiles = new List<Vector2i>(positions);
        var checkQueue = new Queue<Vector2i>();
        checkQueue.Enqueue(remainingTiles[0]);
        
        while (checkQueue.Count > 0) {
            var position = checkQueue.Dequeue();
            var getTile = map.GetTile(position);
            if (!getTile.IsSuccessful) return false;
            var tile = getTile.Value;
            if (!tile.Empty) return false;

            remainingTiles.Remove(position);
            
            void EnqueueAdjacent(Direction direction) {
                var adjacent = position + direction.ToUnitVector();
                if (remainingTiles.Contains(adjacent))
                    checkQueue.Enqueue(adjacent);
            }
            
            EnqueueAdjacent(Direction.Down);
            EnqueueAdjacent(Direction.Left);
            EnqueueAdjacent(Direction.Right);
            EnqueueAdjacent(Direction.Up);
        }

        if (remainingTiles.Count == 0)
            return true;
        return false;
    }

    public Result<HashSet<Tile>> Finish() {
        if (IsContiguousAndValid()) {
            finished = true;
            return new Result<HashSet<Tile>>(
                new HashSet<Tile> (positions.Select(position => map.GetTile(position).Value))
            );
        }
        return new Result<HashSet<Tile>>(new Exception());
    }
}