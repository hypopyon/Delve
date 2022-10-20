using System;
using Delve.Rooms;
using Godot;

namespace Delve;
public partial class Map : Node2D {
    public override void _Draw() {
        DrawSelection();

        for (var i = LeftBound; i <= RightBound; i++)
        for (var j = TopBound; j <= BottomBound; j++)
            DrawConnectors(i, j);
        for (var i = LeftBound; i <= RightBound; i++)
        for (var j = TopBound; j <= BottomBound; j++)
            DrawTile(i, j);
    }

    void DrawConnectors(int x, uint y) {
        var getResult = GetTile(x, y);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        var size = Textures.Connector.GetSize();
        var pos = new Vector2(x * TileWidth, y * TileHeight);
        if (tile.Connectors.Right)
            DrawTexture(Textures.Connector, pos + new Vector2(ConnectorOffset, 0) - size / 2);
        if (tile.Connectors.Up) {
            var angle = -Mathf.Pi / 2;
            DrawSetTransform(pos + new Vector2(0, -ConnectorOffset) - size.Rotated(angle) / 2, angle);
            DrawTexture(Textures.Connector, Vector2.Zero);
        }
        if (tile.Connectors.Left) {
            var angle = Mathf.Pi;
            DrawSetTransform(pos + new Vector2(-ConnectorOffset, 0) - size.Rotated(angle) / 2, angle);
            DrawTexture(Textures.Connector, Vector2.Zero);
        }
        if (tile.Connectors.Down) {
            var angle = Mathf.Pi / 2;
            DrawSetTransform(pos + new Vector2(0, ConnectorOffset) - size.Rotated(angle) / 2, angle);
            DrawTexture(Textures.Connector, Vector2.Zero);
        }
        DrawSetTransform(Vector2.Zero);
    }

    void DrawTile(int x, uint y) {
        var getResult = GetTile(x, y);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        if (tile.Room is not null) {
            var texture = tile.Room.Texture;
            var pos = new Vector2(x * TileWidth, y * TileHeight);
            DrawTexture(texture, pos - texture.GetSize() / 2);
            var stringSize = Fonts.Main.GetStringSize(tile.Room.Name);
            DrawString(Fonts.Main, pos - new Vector2(stringSize.x, -stringSize.y) / 2, tile.Room.Name, modulate: Colors.Fuchsia);
        }
    }
    
    void DrawSelection() {
        if (selectX is null || selectY is null) return;
        var selectRelativeWorldPosition = new Vector2(
            (selectX.Value - .5f) * TileWidth,
            (selectY.Value - .5f) * TileHeight);
        DrawRect(new Rect2(selectRelativeWorldPosition, TileWidth, TileHeight), Colors.Red, true);

        if (selectAdjacentX is not null && selectAdjacentY is not null) {
            var selectAdjacentRelativeWorldPosition = new Vector2(
                (selectAdjacentX.Value - .5f) * TileWidth,
                (selectAdjacentY.Value - .5f) * TileHeight);
            DrawRect(new Rect2(selectAdjacentRelativeWorldPosition, 112, 112), Colors.Green, true);
        }
    }
}