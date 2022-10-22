using Godot;
using System;
using System.Collections.Generic;
using Delve.Rooms;
using DotNext;

namespace Delve;
public partial class Map : Node2D {
    const int Width = 13;
    const int Center = 6;
    const uint InitialHeight = 13;
    const int ConnectorTextureWidth = 16;
    const int TileTextureWidth = 80;
    const int TileTextureHeight = 48;
    public const int SpacedTileWidth = (TileTextureWidth + ConnectorTextureWidth * 2) * TextureScaleFactor;
    public const int SpacedTileHeight = (TileTextureHeight + ConnectorTextureWidth * 2) * TextureScaleFactor;
    public const uint TopBound = 0;
    public const int LeftBound = -Center;
    public const int RightBound = Center;
    const int TextureScaleFactor = 2;
    static Vector2 TextureScale => new Vector2(TextureScaleFactor, TextureScaleFactor);
    static int ConnectorOffsetX => (TileTextureWidth) / 2;
    static int ConnectorOffsetY => (TileTextureHeight) / 2;

    Tile[,] tiles;
    int? selectX, selectAdjacentX;
    uint? selectY, selectAdjacentY;
    //Direction? selectAdjacentDir;
    List<bool> rowEffects = new();
    bool[] columnEffects= Array.Empty<bool>();
    

    public uint BottomBound => Convert.ToUInt32(tiles.GetLength(1) - 1);

    public Map() {
        tiles = new Tile[Width, InitialHeight];
        for (var i = 0; i < Width; i++)
        for (uint j = 0; j < InitialHeight; j++)
            tiles[i, j] = new Tile(this, i - Center, j);
        {
            Tile? lastTile = null;
            for (var i = LeftBound; i <= RightBound; i++) {
                var tile = GetTile(i, 0).Value;
                tile.Room = new Cavern();
                tile.Connectors.Up = true;
                tile.Connectors.Down = true;
                if (lastTile is not null) tile.Connect(lastTile);
                lastTile = tile;
            }
        }
        var entrance = GetTile(0, 0).Value;
        entrance.Room = new Entrance();
    }
    
    public override void _Ready() {
    }

    public Result ExpandDownwards(uint amount = 1) {
        if (amount == 0)
            return new Result(new ArgumentOutOfRangeException());
        var oldHeight = tiles.GetLength(1);
        var newHeight = oldHeight + amount;
        var newTiles = new Tile[Width, newHeight];
        for (var i = 0; i < Width; i++) {
            for (var j = 0; j < oldHeight; j++)
                newTiles[i, j] = tiles[i, j];
            for (var j = oldHeight; j < newHeight; j++)
                newTiles[i, j] = new Tile(this, i, Convert.ToUInt32(j));
        }
        tiles = newTiles;
        return Result.Success;
    }

    public Result<Tile> GetTile(int x, uint y) {
        var adjustedX = x + Center;
        if (adjustedX is < 0 or >= Width || y > BottomBound)
            return new Result<Tile>(new IndexOutOfRangeException());
        return tiles[adjustedX, y];
    }
}
