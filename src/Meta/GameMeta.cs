using System.Collections.Generic;
using Delve.Structures;
using Delve.Units;

namespace Delve;

public partial class GameMeta {
    public StructuresMeta Structures;
    public UnitsMeta Units;

    public GameMeta() {
        Structures = new StructuresMeta();
        Units = new UnitsMeta();
    }
}