using System.Collections.Generic;
using Delve.Structures;
using Delve.Units;

namespace Delve.Meta;

public partial class GameMeta {
    public Dictionary<string, StructureDescription> Structures;
    public Dictionary<string, UnitDescription> Units;

    public GameMeta() {
        DefineStructures();
        DefineUnits();
    }
}