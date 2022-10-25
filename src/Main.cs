using Godot;
using System;
using System.Data;
using Delve.Meta;
using System.Collections.Generic;


namespace Delve;

public partial class Main : Node2D {
	Map map = null!;
	UserInterface userInterface = null!;
	CameraController cameraController = null!;

	GameMeta meta;
	GameState state;


	public Main() {
		
		
	}
	
	public override void _Ready() {
		
	}

	public override void _Process(double delta) {
		/*{ if (GetNode("InterfaceLayer/Interface/MainMargins/Inventory/Resources/Label") is Label label) {
			label.Text = resources.ToString();
			label.QueueRedraw();
		} }
		{ if (GetNode("InterfaceLayer/Interface/MainMargins/Inventory/TradeGoods/Label") is Label label) {
			label.Text = tradeGoods.ToString();
			label.QueueRedraw();
		} }*/
	}

	public override void _UnhandledInput(InputEvent inputEvent) {
		
		if (inputEvent.IsAction("interact_main")) {
			if (!inputEvent.IsPressed()) return;
			//if (Map.hoverTileX is not null && hoverTileY is not null) {
			//	if (hoverTileY > BottomTileBound)
			//		ExpandDownwards(hoverTileY.Value - BottomTileBound);
			//	var tile = GetTile(hoverTileX.Value, hoverTileY.Value);
			//	//tile.Value.Room ??= new Cavern();
			//}

			GetViewport().SetInputAsHandled();
            
		}
	}
}
