using Godot;
using System;

namespace Delve; 

public static class Textures {
    public static readonly Texture2D Connector = (Texture2D)GD.Load("res://Textures/connector.png");
    public static readonly Texture2D Cavern = (Texture2D)GD.Load("res://Textures/tile_Cavern.png");
    public static readonly Texture2D Entrance = (Texture2D)GD.Load("res://Textures/tile_Entrance.png");
}

public static class Fonts {
    public static readonly FontFile Main = (FontFile)GD.Load("res://Fonts/arial.ttf");
}