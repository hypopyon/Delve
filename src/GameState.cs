

namespace Delve;

public class GameState {
    public TurnState Turn;
    public uint Resources, TradeGoods, MaxResources, MaxTradeGoods;
    public Map Map;

    public GameState(Map map) {
        Map = map;
    }
}