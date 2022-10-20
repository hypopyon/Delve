using Godot;

namespace Delve;

public class Connectors {
    public bool Up, Right, Down, Left;

    public Connectors() {
        Up = false;
        Right = false;
        Down = false;
        Left = false;
    }

    public void Draw(Node2D node, Vector2 pos) {
        var size = Textures.Connector.GetSize();
        if (Right)
            node.DrawTexture(Textures.Connector, pos + new Vector2(44, 0) - size / 2);
        if (Up) {
            var angle = -Mathf.Pi / 2;
            node.DrawSetTransform(pos + new Vector2(0, -44) - size.Rotated(angle) / 2, angle);
            node.DrawTexture(Textures.Connector, Vector2.Zero);
        }
        if (Left) {
            var angle = Mathf.Pi;
            node.DrawSetTransform(pos + new Vector2(-44, 0) - size.Rotated(angle) / 2, angle);
            node.DrawTexture(Textures.Connector, Vector2.Zero);
        }
        if (Down) {
            var angle = Mathf.Pi / 2;
            node.DrawSetTransform(pos + new Vector2(0, 44) - size.Rotated(angle) / 2, angle);
            node.DrawTexture(Textures.Connector, Vector2.Zero);
        }
        node.DrawSetTransform(Vector2.Zero);
    }
}