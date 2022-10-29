using Godot;

namespace Delve; 

public partial class MapDrawer {
    void _Draw_InEditor() {
        var texture = Textures.Tiles.Cavern;
        for (var i = GameMap.LeftmostTile; i <= GameMap.RightmostTile; i++)
        for (var j = GameMap.TopmostTile; j < GameMap.InitialTilesHeight; j++) {
            var pos = new Vector2(i * Textures.SpacedTileWidth, j * Textures.SpacedTileHeight);
            DrawSetTransform(pos - texture.GetSize() / 2 * Textures.WorldScaleFactor, 0, Vector2.One * Textures.WorldScaleFactor);
            DrawTexture(Textures.Tiles.Cavern, Vector2.Zero);
        }
    }
}