using System.Collections.Generic;
using Delve.Tiles;

namespace Delve.Structures; 

public class Structure {
    public readonly StructureDefinition Definition;

    public List<Tile> Tiles;
    public Dictionary<string, int>? HousedUnits;
    
    public Structure(StructureDefinition definition, Tile tile) {
        Definition = definition;
        Tiles = new List<Tile>(1) { tile };
    }
    
    public Structure(StructureDefinition definition, HashSet<Tile> uniqueTilesSet) {
        Definition = definition;
        Tiles = new List<Tile>(uniqueTilesSet);
    }
    
}