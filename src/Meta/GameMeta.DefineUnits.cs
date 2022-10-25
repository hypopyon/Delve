using System.Collections.Generic;
using Delve.Units;

namespace Delve.Meta; 

public partial class GameMeta {
    public void DefineUnits() {
        Units = new Dictionary<string, UnitDescription> { };
    }
}