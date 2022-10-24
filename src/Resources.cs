using Godot;
using System;

namespace Delve; 

public static class Textures {
    const string TexturesFolder = "res://assets/textures/";
    public static readonly Texture2D Tunnel = (Texture2D)GD.Load(TexturesFolder + "Tunnel.png");

    public static class Tiles {
        const string TilesTexturesFolder = TexturesFolder + "Tiles/";
        
        public static readonly Texture2D Unexplored = (Texture2D)GD.Load(TilesTexturesFolder + "Unexplored.png");
        public static readonly Texture2D Cavern = (Texture2D)GD.Load(TilesTexturesFolder + "Cavern.png");
        public static readonly Texture2D Entrance = (Texture2D)GD.Load(TilesTexturesFolder + "Entrance.png");
    }
}

public static class Fonts {
    const string FontsFolder = "res://assets/fonts/";
    // To find actual height ratios, use an image editor to test height of power of 2 font sizes.
    // Divide height by fontSize. When using ratio, floor to nearest int value
    public static readonly FontFile Main = (FontFile)GD.Load(FontsFolder + "Arial.ttf");
    public const float MainActualHeightRatio = 0.71875f;
    public static readonly FontFile Mono = (FontFile)GD.Load(FontsFolder + "JetBrainsMono.ttf");
    public const float MonoActualHeightRatio = 0.734375f;
}