using Godot;

namespace Delve; 

public abstract class Room {
    public abstract Texture2D Texture { get; }
    public abstract string Name { get; }

}