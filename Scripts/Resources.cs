using Godot;
using System;

namespace Delve; 

public static class Textures {
    public static readonly Texture2D Connector = (Texture2D)GD.Load("res://Textures/Connector.png");

    public static class Tiles {
        public static readonly Texture2D Unexplored = (Texture2D)GD.Load("res://Textures/Tiles/Unexplored.png");
        public static readonly Texture2D Cavern = (Texture2D)GD.Load("res://Textures/Tiles/Cavern.png");
        public static readonly Texture2D Entrance = (Texture2D)GD.Load("res://Textures/Tiles/Entrance.png");
    }
}

public static class Fonts {
    // To find actual height ratios, use an image editor to test height of power of 2 font sizes.
    // Divide height by fontSize. When using ratio, floor to nearest int value
    public static readonly FontFile Main = (FontFile)GD.Load("res://Fonts/arial.ttf");
    public const float MainActualHeightRatio = 0.71875f;
    public static readonly FontFile Mono = (FontFile)GD.Load("res://Fonts/JetBrainsMono-Regular.ttf");
    public const float MonoActualHeightRatio = 0.734375f;
}