using Godot;
using System;
using System.Collections.Generic;
using Delve.Structures;
using Delve.Tiles;
using DotNext;

namespace Delve;

public class GameMap {
    public const int TilesWidth = 13;
    public const int CenterTileX = 6;
    public const int InitialTilesHeight = 13;
    public const int TopmostTile = 0;
    public const int LeftmostTile = -CenterTileX;
    public const int RightmostTile = CenterTileX;

    Tile[,] tiles;

    public Dictionary<StructureDefinition, List<Structure>> Structures { get; }

    public int BottommostTile {
        get {
            var height = tiles.GetLength(1);
            if (height < 1)
                throw new Exception();
            return height - 1;
        }
    }

public GameMap() {
        Structures = new Dictionary<StructureDefinition, List<Structure>>();
        tiles = new Tile[TilesWidth, InitialTilesHeight];
        for (var i = 0; i < TilesWidth; i++)
        for (var j = 0; j < InitialTilesHeight; j++)
            tiles[i, j] = new Tile(this, new Vector2i(i - CenterTileX, j));
    }
    
    public Result ExpandDownwards(int amount = 1) {
        if (amount == 0)
            return new Result(new ArgumentOutOfRangeException());
        var oldHeight = tiles.GetLength(1);
        var newHeight = oldHeight + amount;
        var newTiles = new Tile[TilesWidth, newHeight];
        for (var i = 0; i < TilesWidth; i++) {
            for (var j = 0; j < oldHeight; j++)
                newTiles[i, j] = tiles[i, j];
            for (var j = oldHeight; j < newHeight; j++)
                newTiles[i, j] = new Tile(this, new Vector2i(i - CenterTileX, j));
        }
        tiles = newTiles;
        return Result.Success;
    }

    public Result<Tile> GetTile(Vector2i position) =>
        IsPositionInBounds(position) ? tiles[position.x + CenterTileX, position.y] : new Result<Tile>(new IndexOutOfRangeException());

    public bool IsPositionInBounds(Vector2i position) =>
        position.x is >= LeftmostTile and <= RightmostTile
        && position.y >= TopmostTile && position.y <= BottommostTile;
}
