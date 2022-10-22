using Godot;

namespace Delve.Rooms; 

public class Entrance : Room {
    public override Texture2D Texture {
        get => Textures.Tiles.Entrance;
    }
    public override string Name {
        get => "Entrance";
    }
}