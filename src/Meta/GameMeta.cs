using System.Collections.Generic;
using Delve.Rooms;
using DotNext;

namespace Delve.Meta;

public class GameMeta {
    public Dictionary<string, RoomDescription> Rooms;
    public Dictionary<string, FixtureDescription> Fixtures;
    public Dictionary<string, UnitDescription> Units;

    public GameMeta() {
        Rooms = new() {
            ["Entrance"] = new RoomDescription {
                Name = "Entrance",
                Texture = Textures.Tiles.Entrance
            },
            ["Cavern"] = new RoomDescription {
                Name = "Cavern",
                Texture = Textures.Tiles.Cavern
            },
            ["Barracks"] = new RoomDescription {
                Name = "Barracks",
                Texture = Textures.Tiles.Cavern,
                Build = new RoomBuildBehavior { ResourceCost = 10, TradeGoodCost = 0 },
                Housing = new SingleUnitTypeHousingBehavior {
                    HousableUnits = new Dictionary<string, int> {
                        ["Soldier"] = 5,
                        ["Gunner"] = 5,
                    }
                }
            },
            ["CannonOutpost"] = new RoomDescription {
                Name = "CannonOutpost",
                Texture = Textures.Tiles.Cavern,
                Build = new RoomBuildBehavior { ResourceCost = 10, TradeGoodCost = 0 },
                Housing = new SingleUnitTypeHousingBehavior {
                    HousableUnits = new Dictionary<string, int> {
                        ["Cannon"] = 2
                    }
                }
            },
            
        };
    }
    
    
}