using System;
using System.Reflection.Metadata;
using Delve.Rooms;
using Godot;

namespace Delve;
public partial class Map : Node2D {
    public override void _Draw() {
        DrawSelection();

        for (var i = LeftTileBound; i <= RightTileBound; i++)
        for (var j = TopTileBound; j <= BottomTileBound; j++)
            DrawConnectors(i, j);
        for (var i = LeftTileBound; i <= RightTileBound; i++)
        for (var j = TopTileBound; j <= BottomTileBound; j++)
            DrawTile(i, j);
        
    }

    void DrawConnectors(int x, uint y) {
        var getResult = GetTile(x, y);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        var textureSize = Textures.Tunnel.GetSize();
        var originOffset = new Vector2(0, textureSize.y / 2);
        var pos = new Vector2(x * SpacedTileWidth, y * SpacedTileHeight);
        if (tile.Connectors.Right) {
            DrawSetTransform(
                pos + (new Vector2(ConnectorOffsetX, 0) - originOffset) * Textures.WorldScaleFactor
                , 0, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Connectors.Up) {
            var angle = -Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, -ConnectorOffsetY) - originOffset.Rotated(angle)) * Textures.WorldScaleFactor,
                angle, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Connectors.Left) {
            DrawSetTransform(
                pos + (new Vector2(-ConnectorOffsetX - textureSize.x, 0) - originOffset) * Textures.WorldScaleFactor,
                0, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Connectors.Down) {
            var angle = Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, ConnectorOffsetY) - originOffset.Rotated(angle)) * Textures.WorldScaleFactor,
                angle, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        DrawSetTransform(Vector2.Zero);
    }

    void DrawTile(int x, uint y) {
        var getResult = GetTile(x, y);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        var texture = tile.Room is not null ? tile.Room.Texture : Textures.Tiles.Unexplored;
        var pos = new Vector2(x * SpacedTileWidth, y * SpacedTileHeight);
        DrawSetTransform(pos - texture.GetSize() / 2 * Textures.WorldScaleFactor, 0, Vector2.One * Textures.WorldScaleFactor);
        DrawTexture(texture, Vector2.Zero);
        DrawSetTransform(Vector2.Zero);
        if (tile.Room is not null) {
            var font = Fonts.Mono;
            var actualHeightRatio = Fonts.MonoActualHeightRatio;
            var fontSize = 16;
            var stringSize = font.GetStringSize(tile.Room.Name, fontSize: fontSize);
            stringSize.y = Mathf.Floor(fontSize * actualHeightRatio);
            
            this.DrawStringZoomCorrected(
                font,
                pos + new Vector2(-stringSize.x / 2, stringSize.y / 2),
                tile.Room.Name,
                modulate: Colors.White,
                fontSize: fontSize);
        }
    }
    
    void DrawSelection() {
        if (hoverTileX is null || hoverTileY is null) return;
        var selectRelativeWorldPosition = new Vector2(
            (hoverTileX.Value - .5f) * SpacedTileWidth,
            (hoverTileY.Value - .5f) * SpacedTileHeight);
        DrawRect(new Rect2(selectRelativeWorldPosition, SpacedTileWidth, SpacedTileHeight), Colors.Red, true);

        if (hoverAdjacentTileX is not null && hoverAdjacentTileY is not null) {
            var selectAdjacentRelativeWorldPosition = new Vector2(
                (hoverAdjacentTileX.Value - .5f) * SpacedTileWidth,
                (hoverAdjacentTileY.Value - .5f) * SpacedTileHeight);
            DrawRect(new Rect2(selectAdjacentRelativeWorldPosition, SpacedTileWidth, SpacedTileHeight), Colors.Green, true);
        }
    }
}