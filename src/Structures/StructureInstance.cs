using System;
using System.Collections.Generic;
using Delve.Tiles;
using Godot;

namespace Delve.Structures; 

public class StructureInstance {
    public readonly StructureDescription Description;

    public List<Tile> Tiles;
    public Dictionary<string, uint>? HousedUnits;
    
    public StructureInstance(StructureDescription description) {
        Description = description;
    }
    
}