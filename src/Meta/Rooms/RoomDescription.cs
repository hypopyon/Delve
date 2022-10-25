using Godot;
using DotNext;
using Delve.Rooms;

namespace Delve.Meta;

public class RoomDescription : ContentDescription {
    public RoomHousingBehavior? Housing;
    public RoomBuildBehavior? Build;
    public RoomEffectBehavior? Effect;
    public Texture2D Texture;


}