

using Delve.Combat;

namespace Delve;

public class GameState {
    public TurnState Turn;
    public CombatState Combat;
    public uint Resources, TradeGoods, MaxResources, MaxTradeGoods;
}