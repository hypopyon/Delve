using Godot;
using System;
using System.Collections.Generic;
using Delve.Tiles;
using DotNext;

namespace Delve;
public class GameMap {
    public const int TilesWidth = 13;
    public const int CenterTile = 6;
    const uint InitialTilesHeight = 13;
    public const uint TopTileBound = 0;
    public const int LeftTileBound = -CenterTile;
    public const int RightTileBound = CenterTile;

    Tile[,] tiles;
    //Direction? selectAdjacentDir;
    

    public uint BottomTileBound => Convert.ToUInt32(tiles.GetLength(1) - 1);

    public GameMap() {
        tiles = new Tile[TilesWidth, InitialTilesHeight];
        for (var i = 0; i < TilesWidth; i++)
        for (uint j = 0; j < InitialTilesHeight; j++)
            tiles[i, j] = new Tile(this, i - CenterTile, j);
    }
    
    public Result ExpandDownwards(uint amount = 1) {
        if (amount == 0)
            return new Result(new ArgumentOutOfRangeException());
        var oldHeight = tiles.GetLength(1);
        var newHeight = oldHeight + amount;
        var newTiles = new Tile[TilesWidth, newHeight];
        for (var i = 0; i < TilesWidth; i++) {
            for (var j = 0; j < oldHeight; j++)
                newTiles[i, j] = tiles[i, j];
            for (var j = oldHeight; j < newHeight; j++)
                newTiles[i, j] = new Tile(this, i, Convert.ToUInt32(j));
        }
        tiles = newTiles;
        return Result.Success;
    }

    public Result<Tile> GetTile(int x, uint y) {
        var adjustedX = x + CenterTile;
        if (adjustedX is < 0 or >= TilesWidth || y > BottomTileBound)
            return new Result<Tile>(new IndexOutOfRangeException());
        return tiles[adjustedX, y];
    }
}
