namespace Delve.Combat; 

public abstract class CombatAction {


    public abstract Result Do(CombatState state);
}