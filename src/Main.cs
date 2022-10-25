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
}
