using Delve.Tiles;
using Godot;
using DotNext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delve.Structures;

public class StructureDescription {
    public string InternalName { get; init; } = null!;
    public string DisplayName { get; init; } = null!;

    public string Name {
        init {
            InternalName = value.Split(' ').Join();
            DisplayName = value;
        }
    }

    public Texture2D Texture { get; init; } = null!;
    public BuildBehavior? Build { get; init; }
    public EffectBehavior? Effect { get; init; }
    public HousingBehavior? Housing { get; init; }
    public int MaxSize { get; init; } = 1;
    public int MinSize { get; init; } = 1;

    public int Size {
        init {
            MaxSize = value;
            MinSize = value;
        }
    }

    public bool IsBuildable => Build is not null;
    public bool HasEffect => Effect is not null;
    public bool HousesUnits => Housing is not null;

    
    public Result<StructureInstance> Create(GameMap map, Tile tile) {
        if (1 < MinSize || 1 > MaxSize)
            return new Result<StructureInstance>(new ArgumentOutOfRangeException());
        var result = new StructureInstance(this, tile);
        tile.Structure = result;
        
        if (!map.Structures.ContainsKey(this))
            map.Structures.Add(this, new List<StructureInstance>());
        map.Structures[this].Add(result);
        return new Result<StructureInstance>(result);
    }
    
    public Result<StructureInstance> Create(GameMap map, HashSet<Tile> tiles) {
        if (tiles.Count < MinSize || tiles.Count > MaxSize)
            return new Result<StructureInstance>(new ArgumentOutOfRangeException());
        var tilesList = tiles.ToList();
        var result = new StructureInstance(this, tiles);
        foreach (var t in tilesList)
            t.Structure = result;
        for (var i = 0; i < tilesList.Count - 1; i++)
        for (var j = i + 1; j < tilesList.Count; j++)
            tilesList[i].Connect(tilesList[j]);
        
        
        if (!map.Structures.ContainsKey(this))
            map.Structures.Add(this, new List<StructureInstance>());
        map.Structures[this].Add(result);
        return new Result<StructureInstance>(result);
    }
}