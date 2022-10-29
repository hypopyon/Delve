using System;
using Delve;
using Godot;

public partial class UserInterface : Control {
	Main main = null!;

	Label resourcesLabel = null!, tradeGoodsLabel = null!;
	
	public override void _Ready() {
		if (GetTree().CurrentScene is not Main getMain)
			throw new Exception();
		main = getMain;
		if (GetNode("MainMargins/Inventory/Resources/Label") is not Label getResourcesLabel)
			throw new Exception();
		resourcesLabel = getResourcesLabel;
		if (GetNode("MainMargins/Inventory/TradeGoods/Label") is not Label getTradeGoodsLabel)
			throw new Exception();
		tradeGoodsLabel = getTradeGoodsLabel;
	}

	public override void _Process(double delta) {
		resourcesLabel.Text = main.State.Resources.ToString();
		tradeGoodsLabel.Text = main.State.TradeGoods.ToString();
		QueueRedraw();
	}

	public override void _Draw() {
		var mousePos = GetLocalMousePosition();
		DrawCircle(mousePos, 16, Colors.Blue);
	}
}
