using Godot;
using System;

public partial class UserInterface : Control
{
	public override void _Process(double delta) {
		QueueRedraw();
	}

	public override void _Draw() {
		var mousePos = GetLocalMousePosition();
		DrawCircle(mousePos, 16, Colors.Blue);
	}
}
