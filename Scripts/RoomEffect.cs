using Godot;
using System;
using System.Collections.Generic;

namespace Delve;

public class RoomEffectMap {
    Dictionary<string, RoomEffect> effects = new();


    //public bool IsEffectActive(string name) {
        
    //}

    //public Result ActivateEffect(string name, RoomEffect effect) {
        
    //}
    
}


public abstract class RoomEffect {
    public abstract string Name { get; }
    public List<Tile> sources;

    public abstract void Activate(RoomEffectMap effectMap);
    public abstract void Deactivate(RoomEffectMap effectMap);
}

public class UndergroundForestHarvestEffect : RoomEffect {
    public override string Name => "Forest";

    public int Harvest => sources.Count;
    
    public override void Activate(RoomEffectMap effectMap) {
        throw new NotImplementedException();
    }

    public override void Deactivate(RoomEffectMap effectMap) {
        throw new NotImplementedException();
    }
}