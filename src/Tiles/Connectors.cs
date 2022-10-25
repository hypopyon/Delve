using Godot;

namespace Delve.Tiles;

public class Connectors {
    public bool Up, Right, Down, Left;

    public Connectors() {
        Up = false;
        Right = false;
        Down = false;
        Left = false;
    }
}