namespace Delve.Structures;

public class BuildBehavior {
    public int ResourcesCost, TradeGoodsCost;
    
    public bool MeetsRequirements(GameState state) {
        return state.Resources >= ResourcesCost && state.TradeGoods >= TradeGoodsCost;
    }

    public void ResolveRequirements(GameState state) {
        state.Resources -= ResourcesCost;
        state.TradeGoods -= TradeGoodsCost;
    }
}