using Godot;
using System;
using System.Data;
using Delve.Meta;
using System.Collections.Generic;
using Delve.Tiles;


namespace Delve;

public partial class Main : Node2D {
	MapDrawer mapDrawer = null!;
	UserInterface userInterface = null!;
	CameraController cameraController = null!;

	public GameMeta Meta;
	public GameState State;
	public GameMap Map;
	
	Tile? hoverTile, hoverAdjacentTile;


	public Main() {
		Meta = new GameMeta();
		State = new GameState();
		Map = new GameMap();
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
		UpdateSelectedTiles();
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
