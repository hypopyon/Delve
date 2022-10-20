using Godot;
namespace Delve.Rooms; 

public class Cavern : Room {
    public override Texture2D Texture {
        get => Textures.Cavern;
    }
    public override string Name {
        get => "Cavern";
    }
}