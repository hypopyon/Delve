using Godot;
using System;
using System.Data;

namespace Delve;

public partial class Main : Node {

	Map map = null!;
	CanvasLayer uiLayer = null!;
	Player player = null!;

	uint resources, tradeGoods;
	
	public override void _Ready() {
		if (GetNode("Map") is Map getMap)
			map = getMap;
		else throw new Exception();
		
		if (GetNode("UILayer") is CanvasLayer getUiLayer)
			uiLayer = getUiLayer;
		else throw new Exception();
		
		if (GetNode("Player") is Player getPlayer)
			getPlayer = player;
		else throw new Exception();
	}

	public override void _Process(double delta) {
		{ if (uiLayer.GetNode("MainMargins/Inventory/Resources/Label") is Label label) {
			label.Text = resources.ToString();
			label.QueueRedraw();
		} }
		{ if (uiLayer.GetNode("MainMargins/Inventory/TradeGoods/Label") is Label label) {
			label.Text = tradeGoods.ToString();
			label.QueueRedraw();
		} }
	}
}
