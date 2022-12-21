using System.Collections.Generic;
using Delve.Structures;

namespace Delve; 

public partial class Definitions {
    public class StructureDefinitions {
        
        public StructureDefinition
            // Special
            Entrance,
            // Natural formations
            Cavern, Forest, CrystalCavern,
            // Buildings
            Barracks;

        public StructureDefinitions() {
            Entrance = new StructureDefinition {
                Name = "Entrance",
                Texture = Textures.Tiles.Entrance,
            };
            Cavern = new StructureDefinition {
                Name = "Cavern",
                Texture = Textures.Tiles.Cavern,
                MaxSize = 4,
            };
            Forest = new StructureDefinition {
                Name = "Forest",
                Texture = Textures.Tiles.Forest,
                Effect = null
            };
            CrystalCavern = new StructureDefinition {
                Name = "Crystal Cavern",
                Texture = Textures.Tiles.CrystalCavern,
                MaxSize = 2,
            };
            Barracks = new StructureDefinition {
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
            };
        }
    }
}