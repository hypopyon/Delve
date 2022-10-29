using System;
using Godot;

namespace Delve; 

[Tool]
public partial class MapDrawer : Node2D {
    Main main = null!;
    GameMap Map => main.Map;
    
    public MapDrawer() {
        TextureFilter = TextureFilterEnum.Nearest;
    }

    public override void _Ready() {
        if (Engine.IsEditorHint())
            return;
        else _Ready_InGame();
    }

    public override void _Process(double delta) {
        if (Engine.IsEditorHint())
            return;
        else _Process_InGame(delta);
    }

    public override void _Draw() {
        if (Engine.IsEditorHint())
            _Draw_InEditor();
        else _Draw_InGame();
    }
}