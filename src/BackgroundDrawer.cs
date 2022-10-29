using Godot;
using System;
using Delve;

namespace Delve;

[Tool]
public partial class BackgroundDrawer : Node2D {
	Main main = null!;
	GameMap Map => main.Map;

	public BackgroundDrawer() {
		TextureFilter = TextureFilterEnum.Nearest;
	}
	
	public override void _Ready() {
		if (Engine.IsEditorHint() == false)
			_Ready_InGame();
	}

	public override void _Process(double delta) {
		if (Engine.IsEditorHint() == false)
			_Process_InGame(delta);
	}

	public override void _Draw() {
		if (Engine.IsEditorHint())
			_Draw_InEditor();
		else _Draw_InGame();
	}
}
