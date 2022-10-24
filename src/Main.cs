using Godot;
using System;
using System.Data;

namespace Delve;

public enum TurnState {
	None,
	Harvest,
	Explore,
	Trade,
	Build,
	Recruit,
	Activate
}

public partial class Main : Node2D {

	Map map = null!;
	Control interfaceNode = null!;
	CameraController cameraController = null!;

	TurnState turn;
	RoomEffectMap roomEffectMap;
	uint resources, tradeGoods, maxResources, maxTradeGoods;

	public Main() {
		turn = TurnState.None;
		roomEffectMap = new RoomEffectMap();
		resources = 20;
		tradeGoods = 20;
		maxResources = 50;
		maxTradeGoods = 50;
	}
	
	public override void _Ready() {
		
	}

	public override void _Process(double delta) {
		{ if (GetNode("InterfaceLayer/Interface/MainMargins/Inventory/Resources/Label") is Label label) {
			label.Text = resources.ToString();
			label.QueueRedraw();
		} }
		{ if (GetNode("InterfaceLayer/Interface/MainMargins/Inventory/TradeGoods/Label") is Label label) {
			label.Text = tradeGoods.ToString();
			label.QueueRedraw();
		} }
		QueueRedraw();
	}
}
