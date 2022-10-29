using Godot;
using System;
using Delve.Tiles;
using DotNext;


namespace Delve;

public partial class Main : Node2D {
	BackgroundDrawer backgroundDrawer = null!;
	MapDrawer mapDrawer = null!;
	CameraController cameraController = null!;
	UserInterface userInterface = null!;
	
	public GameMeta Meta { get; private set; }
	public GameState State { get; private set; }
	public GameMap Map { get; private set; }

	public Tile? hoverTile;
	public Tile? hoverAdjacentTile;
	
	RandomNumberGenerator rng;


	public Main() {
		Meta = new GameMeta();
		State = new GameState();
		State.Turn = TurnState.Explore;
		Map = new GameMap();

		var entrance = Map.GetTile(Vector2i.Zero).Value;
		Meta.Structures.Entrance.Create(Map, entrance);
		
		rng = new RandomNumberGenerator();
	}

	public override void _Ready() {
		GetNodeAssign("BackgroundLayer/BackgroundDrawer", out backgroundDrawer);
		GetNodeAssign("MapLayer/MapDrawer", out mapDrawer);
		GetNodeAssign("CameraControllerLayer/CameraController", out cameraController);
		GetNodeAssign("UserInterfaceLayer/UserInterface", out userInterface);
	}

	public override void _Process(double delta) {
		void UpdateInventoryLabel(string path, int value) {
			if (GetNode("UserInterfaceLayer/UserInterface/MainMargins/Inventory/" + path + "/Label") is not Label label)
				return;
			label.Text = value.ToString();
			label.QueueRedraw();
		}
		UpdateInventoryLabel("Resources", State.Resources);
		UpdateInventoryLabel("TradeGoods", State.TradeGoods);

		if (Input.IsKeyPressed(Key.R))
			GetTree().ReloadCurrentScene();
		
		UpdateSelectedTiles();
	}

	public override void _UnhandledInput(InputEvent inputEvent) {
		if (inputEvent.IsAction("interact_main") && inputEvent.IsPressed()) {

			Result? explore = null;

			if (State.Turn == TurnState.Explore && !State.InCombat) {
				TryExplore();
			}
		}
	}

	void TryExplore() {
		Result? exploreResult = null;
		if (hoverTile is not null) {
			if (hoverTile.Empty) {
				var countAdjacentStructures = hoverTile.CountAdjacentStructures();
				if (countAdjacentStructures.Count == 1
				    && countAdjacentStructures.FirstAdjacentStructure is not null
				    && countAdjacentStructures.FirstAdjacentStructure.Empty == false)
					exploreResult = Explore(countAdjacentStructures.FirstAdjacentStructure, hoverTile);
				else if (hoverAdjacentTile is not null && hoverAdjacentTile.Empty == false)
					exploreResult = Explore(hoverAdjacentTile, hoverTile);
			} else if (hoverAdjacentTile is not null && hoverAdjacentTile.Empty)
				exploreResult = Explore(hoverTile, hoverAdjacentTile);
		}
		if (exploreResult is not null && exploreResult.Value.IsSuccessful == false)
			throw exploreResult.Value.Error;
	}
	
	void GetNodeAssign<T>(string path, out T result) => NodeExtensions.GetNodeAssign(this, path, out result);
}
