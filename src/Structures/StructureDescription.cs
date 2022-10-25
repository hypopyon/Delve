using Godot;

namespace Delve.Structures;

public class StructureDescription {
    public string Name;
    public Texture2D Texture;
    public BuildBehavior? Build;
    public EffectBehavior? Effect;
    public HousingBehavior? Housing;

    public bool IsBuildable => Build is not null;
    public bool HasEffect => Effect is not null;
    public bool HousesUnits => Housing is not null;
}