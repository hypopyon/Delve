

using Delve.Combat;

namespace Delve;

public class GameState {
    public TurnState Turn;
    public CombatState? Combat;
    public int Resources, TradeGoods, MaxResources, MaxTradeGoods;

    public bool InCombat => Combat is not null;

    public GameState() {
        Turn = TurnState.None;
        Combat = null;
        MaxResources = 50;
        MaxTradeGoods = 50;
        Resources = 20;
        TradeGoods = 20;
    }
}