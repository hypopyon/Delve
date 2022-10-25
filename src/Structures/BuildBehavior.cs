namespace Delve.Structures;

public class BuildBehavior {
    public uint ResourcesCost, TradeGoodsCost;
    
    public bool MeetsRequirements(GameState state) {
        return state.Resources >= ResourcesCost && state.TradeGoods >= TradeGoodsCost;
    }

    public void ResolveRequirements(GameState state) {
        state.Resources -= ResourcesCost;
        state.TradeGoods -= TradeGoodsCost;
    }
}