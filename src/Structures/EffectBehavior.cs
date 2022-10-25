using System;
using System.Collections.Generic;
using Delve.Tiles;

namespace Delve.Structures;


public abstract class EffectBehavior {
    public abstract string Name { get; }
    public List<Tile> sources;

    public abstract void Activate();
    public abstract void Deactivate();
}