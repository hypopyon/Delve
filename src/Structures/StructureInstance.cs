using System.Collections.Generic;
using Delve.Tiles;

namespace Delve.Structures; 

public class StructureInstance {
    public readonly StructureDescription Description;

    public List<Tile> Tiles;
    public Dictionary<string, int>? HousedUnits;
    
    public StructureInstance(StructureDescription description, Tile tile) {
        Description = description;
        Tiles = new List<Tile>(1) { tile };
    }
    
    public StructureInstance(StructureDescription description, HashSet<Tile> uniqueTilesSet) {
        Description = description;
        Tiles = new List<Tile>(uniqueTilesSet);
    }
    
}