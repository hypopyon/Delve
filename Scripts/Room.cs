using Godot;

namespace Delve; 

public abstract class Room {
    public abstract Texture2D Texture { get; }
    public abstract string Name { get; }

    public void Draw(Node2D node, Vector2 pos) {
        node.DrawTexture(Texture, pos - Texture.GetSize() / 2);
        var stringSize = Fonts.Main.GetStringSize(Name);
        node.DrawString(Fonts.Main, pos - new Vector2(stringSize.x, -stringSize.y) / 2, Name, modulate: Colors.Fuchsia);
    }
}