using Godot;
using System;
using Delve.Rooms;
using DotNext;

namespace Delve;
public partial class Map : Node2D {
    const int WIDTH = 13;
    const int CENTER = 6;
    const uint INITIAL_HEIGHT = 13;
    
    Tile[,] tiles;
    public int LeftBound => -CENTER;
    public int RightBound => CENTER;
    public const uint TopBound = 0;
    public uint BottomBound => Convert.ToUInt32(tiles.GetLength(1) - 1);

    int? selectX, selectAdjacentX;
    uint? selectY, selectAdjacentY;
    Direction? selectAdjacentDir;
    

    public Map() {
        tiles = new Tile[WIDTH, INITIAL_HEIGHT];
        for (var i = 0; i < WIDTH; i++)
        for (uint j = 0; j < INITIAL_HEIGHT; j++)
            tiles[i, j] = new Tile(this, i - CENTER, j);
    }

    public void ExpandDownwards(uint amount = 1) {
        if (amount == 0)
            throw new ArgumentOutOfRangeException();
        var oldHeight = tiles.GetLength(1);
        var newHeight = oldHeight + amount;
        var newTiles = new Tile[WIDTH, newHeight];
        for (var i = 0; i < WIDTH; i++) {
            for (var j = 0; j < oldHeight; j++)
                newTiles[i, j] = tiles[i, j];
            for (var j = oldHeight; j < newHeight; j++)
                newTiles[i, j] = new Tile(this, i, Convert.ToUInt32(j));
        }
        tiles = newTiles;
    }

    public Result<Tile> GetTile(int x, uint y) {
        var adjustedX = x + CENTER;
        if (adjustedX is < 0 or >= WIDTH || y > BottomBound)
            return new Result<Tile>(new IndexOutOfRangeException());
        return tiles[adjustedX, y];
    }

    public override void _Ready() {
        Tile? lastTile = null;
        for (var i = LeftBound; i <= RightBound; i++) {
            var tile = GetTile(i, 0).Value;
            tile.Room = new Cavern();
            if (lastTile is not null) tile.Connect(lastTile);
            lastTile = tile;
        }

        {
            var tile = GetTile(0, 0).Value;
            tile.Room = new Entrance();
        }
    }
}
