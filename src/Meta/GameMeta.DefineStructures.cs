using System.Collections.Generic;
using Delve.Structures;

namespace Delve.Meta; 

public partial class GameMeta {
    public void DefineStructures() {
        Structures = new() {
            ["Entrance"] = new StructureDescription {
                Name = "Entrance",
                Texture = Textures.Tiles.Entrance
            },
            ["Cavern"] = new StructureDescription {
                Name = "Cavern",
                Texture = Textures.Tiles.Cavern
            },
            ["Barracks"] = new StructureDescription {
                Name = "Barracks",
                Texture = Textures.Tiles.Cavern,
                Build = new BuildBehavior {
                    ResourcesCost = 15
                },
                Housing = new HousingBehavior {
                    HousableUnits = new Dictionary<string, int> {
                        ["Soldier"] = 5,
                        ["Gunner"] = 5,
                    }
                }
            },
        };
    }
}