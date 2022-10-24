using System;
using Delve.Rooms;
using Godot;



namespace Delve;
public partial class Map : Node2D {
    public override void _Input(InputEvent @event) {
        
    }
    
    public override void _UnhandledInput(InputEvent inputEvent) {
        if (inputEvent.IsAction("interact_main")) {
            if (!inputEvent.IsPressed()) return;
            if (hoverTileX is not null && hoverTileY is not null) {
                if (hoverTileY > BottomBound)
                    ExpandDownwards(hoverTileY.Value - BottomBound);
                var tile = GetTile(hoverTileX.Value, hoverTileY.Value);
                tile.Value.Room ??= new Cavern();
            }

            var viewport = GetViewport();
            viewport.SetInputAsHandled();
            
        }
    }
}