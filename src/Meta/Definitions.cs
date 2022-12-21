using System.Collections.Generic;
using Delve.Structures;
using Delve.Units;

namespace Delve;

public partial class Definitions {
    public StructureDefinitions Structures;
    public UnitDefinitions Units;

    public Definitions() {
        Structures = new StructureDefinitions();
        Units = new UnitDefinitions();
    }
}