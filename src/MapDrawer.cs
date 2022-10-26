using System;
using Godot;

namespace Delve; 

public partial class MapDrawer : Node2D {
    
    Main main = null!;
    GameMap Map => main.Map;

    public MapDrawer() {
        TextureFilter = TextureFilterEnum.Nearest;
    }
    
    public override void _Ready() {
        if (GetTree().CurrentScene is not Main getMain)
            throw new Exception();
        main = getMain;
    }

    public override void _Draw() {
        //DrawSelection();
        for (var i = GameMap.LeftTileBound; i <= GameMap.RightTileBound; i++)
        for (var j = GameMap.TopTileBound; j <= Map.BottomTileBound; j++)
            DrawConnectors(i, j);
        for (var i = GameMap.LeftTileBound; i <= GameMap.RightTileBound; i++)
        for (var j = GameMap.TopTileBound; j <= Map.BottomTileBound; j++)
            DrawTile(i, j);
    }
    
    void DrawConnectors(int x, uint y) {
        var getResult = Map.GetTile(x, y);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        var textureSize = Textures.Tunnel.GetSize();
        var originOffset = new Vector2(0, textureSize.y / 2);
        var pos = new Vector2(x * Textures.SpacedTileWidth, y * Textures.SpacedTileHeight);
        if (tile.Connectors.Right) {
            DrawSetTransform(
                pos + (new Vector2(Textures.TunnelOffsetX, 0) - originOffset) * Textures.WorldScaleFactor
                , 0, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Connectors.Up) {
            var angle = -Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, -Textures.TunnelOffsetY) - originOffset.Rotated(angle)) * Textures.WorldScaleFactor,
                angle, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Connectors.Left) {
            DrawSetTransform(
                pos + (new Vector2(-Textures.TunnelOffsetX - textureSize.x, 0) - originOffset) * Textures.WorldScaleFactor,
                0, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        if (tile.Connectors.Down) {
            var angle = Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, Textures.TunnelOffsetY) - originOffset.Rotated(angle)) * Textures.WorldScaleFactor,
                angle, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tunnel, Vector2.Zero);
        }
        DrawSetTransform(Vector2.Zero);
    }

    void DrawTile(int x, uint y) {
        var getResult = Map.GetTile(x, y);
        if (!getResult.IsSuccessful)
            throw new InvalidOperationException();
        var tile = getResult.Value;
        var pos = new Vector2(x * Textures.SpacedTileWidth, y * Textures.SpacedTileHeight);
        var texture = tile.Texture;
        DrawSetTransform(pos - texture.GetSize() / 2 * Textures.WorldScaleFactor, 0, Vector2.One * Textures.WorldScaleFactor);
        DrawTexture(texture, Vector2.Zero);
        DrawSetTransform(Vector2.Zero);
        if (tile.Structure is not null) {
            var font = Fonts.Mono;
            var actualHeightRatio = Fonts.MonoActualHeightRatio;
            var fontSize = 16;
            var stringSize = font.GetStringSize(tile.Structure.Description.Name, fontSize: fontSize);
            stringSize.y = Mathf.Floor(fontSize * actualHeightRatio);
            
            this.DrawStringZoomCorrected(
                font,
                pos + new Vector2(-stringSize.x / 2, stringSize.y / 2),
                tile.Structure.Description.Name,
                modulate: Colors.White,
                fontSize: fontSize);
        }
    }
    
    /*void DrawSelection() {
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
    }*/
}