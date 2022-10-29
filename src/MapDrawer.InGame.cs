using Godot;
using System;

namespace Delve; 

public partial class MapDrawer {
    void _Ready_InGame() {
        if (GetTree().CurrentScene is not Main getMain)
            throw new Exception();
        main = getMain;
    }
    
    void _Process_InGame(double delta) {
        QueueRedraw();
    }

    void _Draw_InGame() {
        DrawSelection();
        for (var i = GameMap.LeftmostTile; i <= GameMap.RightmostTile; i++)
        for (var j = GameMap.TopmostTile; j <= Map.BottommostTile; j++)
            DrawConnectors(new Vector2i(i, j));
        for (var i = GameMap.LeftmostTile; i <= GameMap.RightmostTile; i++)
        for (var j = GameMap.TopmostTile; j <= Map.BottommostTile; j++)
            DrawTile(new Vector2i(i, j));
    }
        
    void DrawConnectors(Vector2i position) {
        var getResult = Map.GetTile(position);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        var textureSize = Textures.Tunnel.GetSize();
        var originOffset = new Vector2(0, textureSize.y / 2);
        var pos = new Vector2(position.x * Textures.SpacedTileWidth, position.y * Textures.SpacedTileHeight);
        if (tile.Tunnels.Right) {
            DrawSetTransform(
                pos + (new Vector2(Textures.TunnelOffsetX, 0) - originOffset) * Textures.WorldScaleFactor
                , 0, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Tunnels.Up) {
            const float angle = -Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, -Textures.TunnelOffsetY) - originOffset.Rotated(angle)) * Textures.WorldScaleFactor,
                angle, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Tunnels.Left) {
            DrawSetTransform(
                pos + (new Vector2(-Textures.TunnelOffsetX - textureSize.x, 0) - originOffset) * Textures.WorldScaleFactor,
                0, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Tunnels.Down) {
            const float angle = Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, Textures.TunnelOffsetY) - originOffset.Rotated(angle)) * Textures.WorldScaleFactor,
                angle, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        DrawSetTransform(Vector2.Zero);
    }

    void DrawTile(Vector2i position) {
        var getResult = Map.GetTile(position);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        var pos = new Vector2(position.x * Textures.SpacedTileWidth, position.y * Textures.SpacedTileHeight);
        var texture = tile.Texture;
        DrawSetTransform(pos - texture.GetSize() / 2 * Textures.WorldScaleFactor, 0, Vector2.One * Textures.WorldScaleFactor);
        DrawTexture(texture, Vector2.Zero);
        DrawSetTransform(Vector2.Zero);
        if (tile.Structure is null) return;
        var font = Fonts.Mono;
        var actualHeightRatio = Fonts.MonoActualHeightRatio;
        var fontSize = 16;
        var stringSize = font.GetStringSize(tile.Structure.Description.DisplayName, fontSize: fontSize);
        stringSize.y = Mathf.Floor(fontSize * actualHeightRatio);
            
        this.DrawStringZoomCorrected(
            font,
            pos + new Vector2(-stringSize.x / 2, stringSize.y / 2),
            tile.Structure.Description.DisplayName,
            modulate: Colors.White,
            fontSize: fontSize);
    }
    
    void DrawSelection() {
        if (main.hoverTile is null) return;
        var hoverTileRelativeWorldPosition = new Vector2(
            (main.hoverTile.X - .5f) * Textures.SpacedTileWidth,
            (main.hoverTile.Y - .5f) * Textures.SpacedTileHeight);
        DrawRect(new Rect2(hoverTileRelativeWorldPosition, Textures.SpacedTileWidth, Textures.SpacedTileHeight), new Color(1, 0, 0, 0.5f));

        if (main.hoverAdjacentTile is null) return;
        var hoverAdjacentTileRelativeWorldPosition = new Vector2(
            (main.hoverAdjacentTile.X - .5f) * Textures.SpacedTileWidth,
            (main.hoverAdjacentTile.Y - .5f) * Textures.SpacedTileHeight);
        DrawRect(new Rect2(hoverAdjacentTileRelativeWorldPosition, Textures.SpacedTileWidth, Textures.SpacedTileHeight), new Color(0, 1, 0, 0.5f));
    }
}