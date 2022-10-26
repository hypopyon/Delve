using Godot;

namespace Delve.Structures;

public class StructureDescription {
    public string Name { get; init; }
    public Texture2D Texture { get; init; }
    public BuildBehavior? Build { get; init; }
    public EffectBehavior? Effect { get; init; }
    public HousingBehavior? Housing { get; init; }

    public bool IsBuildable => Build is not null;
    public bool HasEffect => Effect is not null;
    public bool HousesUnits => Housing is not null;
}