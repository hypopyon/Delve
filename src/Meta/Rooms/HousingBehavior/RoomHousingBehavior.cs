using System.Collections.Generic;

namespace Delve.Meta;

public abstract class RoomHousingBehavior {
    public abstract Dictionary<string, int> GetUnitCapacities();
    public abstract Dictionary<string, int> GetUnitRemainingCapacities();
    public abstract Dictionary<string, int> GetUnitAmounts();
    public abstract string[] GetAssignableUnits();
    public abstract int GetUnitCapacity(string unitType);
    public abstract int GetUnitRemainingCapacity(string unitType);
    public abstract int GetUnitAmount(string unitType);
    public abstract bool IsUnitAssignable(string unitType);
    public abstract Result AssignUnits(Tile tile, string unitType, int amount);
    public abstract Result RemoveUnits(Tile tile, string unitType, int amount);
}