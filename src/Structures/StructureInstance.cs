using System;
using System.Collections.Generic;
using Delve.Tiles;

namespace Delve.Structures; 

public class StructureInstance {
    public readonly Tile Tile;
    public readonly StructureDescription Description;
    public Dictionary<string, uint>? HousedUnits;
    
    public StructureInstance(Tile tile, StructureDescription description) {
        Tile = tile;
        Description = description;
    }
    
    public void AssignUnits() {
        if (Description.Housing is null)
            throw new Exception();
    }
}