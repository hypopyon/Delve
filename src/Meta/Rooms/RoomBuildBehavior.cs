using System;
using System.Collections.Generic;
using System.Linq;


namespace Delve.Rooms;

public abstract class RoomBuildRequirement {
    public abstract bool MeetsRequirement(GameState state, Tile tile);
    public abstract void ResolveRequirement(GameState state, Tile tile);
}

public class MaterialCostBuildRequirement : RoomBuildRequirement {
    public readonly uint ResourceCost, TradeGoodCost;
    public override bool MeetsRequirement(GameState state, Tile tile) {
        return state.Resources >= ResourceCost && state.TradeGoods >= TradeGoodCost;
    }

    public override void ResolveRequirement(GameState state, Tile tile) {
        state.Resources -= ResourceCost;
        state.TradeGoods -= TradeGoodCost;
    }
}

// FoodBuildRequirement, WaterBuildRequirement


public class RoomBuildBehavior {
    public List<RoomBuildRequirement> Requirements;
    public bool MeetsRequirements(GameState state, Tile tile) {
        return Requirements.All(i => i.MeetsRequirement(state, tile));
    }

    public void ResolveRequirements(GameState state, Tile tile) {
        foreach (var i in Requirements) {
            if (i.MeetsRequirement(state, tile))
                i.ResolveRequirement(state, tile);
            else throw new Exception();
        }
    }
}