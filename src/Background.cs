using Godot;
using System;
using Delve;

public partial class Background : Node2D {
	Map map = null!;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		if (GetNode("../../MapLayer/Map") is Map getMap)
			map = getMap;
		else throw new Exception();
		TextureFilter = TextureFilterEnum.Nearest;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public override void _Draw() {
		var textureSize = Textures.Background.GetSize();
		var x = Map.SpacedTileWidth * (-Map.CenterTile - 0.5f);
		var y = -Map.SpacedTileHeight / 2;
		var width = Map.SpacedTileWidth * Map.TilesWidth;
		var height = Map.SpacedTileHeight * (map.BottomTileBound + 1);
		DrawSetTransform(new Vector2(x, y), scale: new Vector2(Textures.WorldScaleFactor, Textures.WorldScaleFactor));
		DrawTextureRect(Textures.Background, new Rect2(
			0, 0,
			width / Convert.ToSingle(Textures.WorldScaleFactor),
			height / Convert.ToSingle(Textures.WorldScaleFactor)
			), true);
	}
}
