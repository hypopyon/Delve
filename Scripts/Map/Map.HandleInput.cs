using System;
using Delve.Rooms;
using Godot;

namespace Delve;

public partial class Map : Node2D {
    void HandleInput() {
        UpdateSelectedTiles();
        if (Input.IsActionJustPressed("interact_main")) {
            if (selectX is not null && selectY is not null) {
                if (selectY > BottomBound)
                    ExpandDownwards(selectY.Value - BottomBound);
                var tile = GetTile(selectX.Value, selectY.Value);
                tile.Value.Room ??= new Cavern();
            }
        }
    }
}