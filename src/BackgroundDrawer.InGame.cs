using System;
using Godot;

namespace Delve;

public partial class BackgroundDrawer {


    void _Ready_InGame() {
        if (GetTree().CurrentScene is not Main getMain)
            throw new Exception();
        main = getMain;
    }

    void _Process_InGame(double delta) {
        QueueRedraw();
    }

    void _Draw_InGame() {
        var textureSize = Textures.Background.GetSize();
        var x = Textures.SpacedTileWidth * (-GameMap.CenterTileX - 0.5f);
        var y = -Textures.SpacedTileHeight / 2;
        var width = Textures.SpacedTileWidth * GameMap.TilesWidth;
        var height = Textures.SpacedTileHeight * (Map.BottommostTile + 1);
        DrawSetTransform(new Vector2(x, y), scale: new Vector2(Textures.WorldScaleFactor, Textures.WorldScaleFactor));
        DrawTextureRect(Textures.Background, new Rect2(
            0, 0,
            width / Convert.ToSingle(Textures.WorldScaleFactor),
            height / Convert.ToSingle(Textures.WorldScaleFactor)
        ), true);
    }
}