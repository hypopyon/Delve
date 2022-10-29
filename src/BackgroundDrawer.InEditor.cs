using System;
using Godot;

namespace Delve;

public partial class BackgroundDrawer {
    void _Draw_InEditor() {
        var textureSize = Textures.Background.GetSize();
        var x = Textures.SpacedTileWidth * (-GameMap.CenterTileX - 0.5f);
        var y = -Textures.SpacedTileHeight / 2;
        var width = Textures.SpacedTileWidth * GameMap.TilesWidth;
        var height = Textures.SpacedTileHeight * GameMap.InitialTilesHeight;
        DrawSetTransform(new Vector2(x, y), scale: new Vector2(Textures.WorldScaleFactor, Textures.WorldScaleFactor));
        DrawTextureRect(Textures.Background, new Rect2(
            0, 0,
            width / Convert.ToSingle(Textures.WorldScaleFactor),
            height / Convert.ToSingle(Textures.WorldScaleFactor)
        ), true);
    }
}