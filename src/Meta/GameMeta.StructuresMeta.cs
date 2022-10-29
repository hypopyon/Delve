using System.Collections.Generic;
using Delve.Structures;

namespace Delve; 

public partial class GameMeta {
    public class StructuresMeta {
        
        public StructureDescription
            // Special
            Entrance,
            // Natural formations
            Cavern, Forest, CrystalCavern,
            // Buildings
            Barracks;

        public StructuresMeta() {
            Entrance = new StructureDescription {
                Name = "Entrance",
                Texture = Textures.Tiles.Entrance,
            };
            Cavern = new StructureDescription {
                Name = "Cavern",
                Texture = Textures.Tiles.Cavern,
                MaxSize = 4,
            };
            Forest = new StructureDescription {
                Name = "Forest",
                Texture = Textures.Tiles.Forest,
                Effect = null
            };
            CrystalCavern = new StructureDescription {
                Name = "Crystal Cavern",
                Texture = Textures.Tiles.CrystalCavern,
                MaxSize = 2,
            };
            Barracks = new StructureDescription {
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