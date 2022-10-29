

using System;
using DotNext;
using System.Collections.Generic;
using Godot;

namespace Delve.Structures; 

public class StructurePlacementBuffer {
    GameMap map;
    StructureDescription description;
    List<Vector2i> tiles;
    uint lowestTile;
    
    public StructurePlacementBuffer(GameMap map, StructureDescription description) {
        this.map = map;
    }

    public Result Add(int x, uint y) {

        return new Result(new Exception());
    }

    public Result<StructureInstance> Create() {
        return new Result<StructureInstance>(new Exception());
    }
}