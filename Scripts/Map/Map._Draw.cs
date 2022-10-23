using System;
using System.Reflection.Metadata;
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
        var originOffset = new Vector2(0, Textures.Connector.GetSize().y / 2);
        var pos = new Vector2(x * SpacedTileWidth, y * SpacedTileHeight);
        if (tile.Connectors.Right) {
            DrawSetTransform(
                pos + (new Vector2(ConnectorOffsetX, 0) - originOffset) * TextureScale
                , 0, TextureScale);
            DrawTexture(Textures.Connector, Vector2.Zero);
        }
        if (tile.Connectors.Up) {
            var angle = -Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, -ConnectorOffsetY) - originOffset.Rotated(angle)) * TextureScale,
                angle, TextureScale);
            DrawTexture(Textures.Connector, Vector2.Zero);
        }
        if (tile.Connectors.Left) {
            var angle = Mathf.Pi;
            DrawSetTransform(
                pos + (new Vector2(-ConnectorOffsetX, 0) - originOffset.Rotated(angle)) * TextureScale,
                angle, TextureScale);
            DrawTexture(Textures.Connector, Vector2.Zero);
        }
        if (tile.Connectors.Down) {
            var angle = Mathf.Pi / 2;
            DrawSetTransform(
                pos + (new Vector2(0, ConnectorOffsetY) - originOffset.Rotated(angle)) * TextureScale,
                angle, TextureScale);
            DrawTexture(Textures.Connector, Vector2.Zero);
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
        DrawSetTransform(pos - texture.GetSize() / 2 * TextureScale, 0, TextureScale);
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
        if (selectX is null || selectY is null) return;
        var selectRelativeWorldPosition = new Vector2(
            (selectX.Value - .5f) * SpacedTileWidth,
            (selectY.Value - .5f) * SpacedTileHeight);
        DrawRect(new Rect2(selectRelativeWorldPosition, SpacedTileWidth, SpacedTileHeight), Colors.Red, true);

        if (selectAdjacentX is not null && selectAdjacentY is not null) {
            var selectAdjacentRelativeWorldPosition = new Vector2(
                (selectAdjacentX.Value - .5f) * SpacedTileWidth,
                (selectAdjacentY.Value - .5f) * SpacedTileHeight);
            DrawRect(new Rect2(selectAdjacentRelativeWorldPosition, SpacedTileWidth, SpacedTileHeight), Colors.Green, true);
        }
    }
}