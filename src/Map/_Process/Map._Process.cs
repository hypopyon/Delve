using Godot;

namespace Delve;
public partial class Map : Node2D {
    public override void _Process(double delta) {
        UpdateSelectedTiles();
        QueueRedraw();
    }
}