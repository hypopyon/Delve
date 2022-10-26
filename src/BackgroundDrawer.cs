using Godot;
using System;
using Delve;

public partial class BackgroundDrawer : Node2D {
	Main main = null!;
	GameMap Map => main.Map;

	public BackgroundDrawer() {
		TextureFilter = TextureFilterEnum.Nearest;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		if (GetTree().CurrentScene is not Main getMain)
			throw new Exception();
		main = getMain;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public override void _Draw() {
		var textureSize = Textures.Background.GetSize();
		var x = Textures.SpacedTileWidth * (-GameMap.CenterTile - 0.5f);
		var y = -Textures.SpacedTileHeight / 2;
		var width = Textures.SpacedTileWidth * GameMap.TilesWidth;
		var height = Textures.SpacedTileHeight * (Map.BottomTileBound + 1);
		DrawSetTransform(new Vector2(x, y), scale: new Vector2(Textures.WorldScaleFactor, Textures.WorldScaleFactor));
		DrawTextureRect(Textures.Background, new Rect2(
			0, 0,
			width / Convert.ToSingle(Textures.WorldScaleFactor),
			height / Convert.ToSingle(Textures.WorldScaleFactor)
			), true);
	}
}
