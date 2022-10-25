using System.Collections.Generic;

namespace Delve.Meta.Rooms; 

public class RoomInstance {
    public readonly Tile Tile;
    public readonly RoomDescription Description;
    public string[]? AssignableUnits => Description.Housing?.GetAssignableUnits();

    public RoomInstance(Tile tile, RoomDescription description) {
        Tile = tile;
        Description = description;
    }
    
    public void AssignUnits() {
        if (Description.Housing is null)
            return;
        Description.Housing.AssignUnits(Tile, "Soldier", 1);
    }
}