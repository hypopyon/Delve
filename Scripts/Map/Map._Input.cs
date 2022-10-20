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
            if (selectX is not null && selectY is not null) {
                if (selectY > BottomBound)
                    ExpandDownwards(selectY.Value - BottomBound);
                var tile = GetTile(selectX.Value, selectY.Value);
                tile.Value.Room ??= new Cavern();
            }

            var viewport = GetViewport();
            viewport.SetInputAsHandled();
            
        }
    }
}