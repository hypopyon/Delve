using System;
using Delve.Rooms;
using Godot;

namespace Delve;
public partial class Map : Node2D {
    public override void _Draw() {
        if (selectX is not null && selectY is not null) {
            var selectWorldPosition = new Vector2(Convert.ToSingle(selectX) * 112, Convert.ToSingle(selectY) * 112);
            DrawRect(new Rect2(selectWorldPosition - new Vector2(56, 56), 112, 112), Colors.Red, true);

            if (selectAdjacentX is not null && selectAdjacentY is not null) {
                var selectAdjacentWorldPosition = new Vector2(
                    Convert.ToSingle(selectAdjacentX) * 112 - 56,
                    Convert.ToSingle(selectAdjacentY) * 112 - 56
                );
                DrawRect(new Rect2(selectAdjacentWorldPosition, 112, 112), Colors.Green, true);
            }
        }

        for (var i = LeftBound; i <= RightBound; i++)
        for (var j = TopBound; j <= BottomBound; j++)
            GetTile(i, j).Value.Connectors.Draw(this, new Vector2(i * 112, j * 112));
        for (var i = LeftBound; i <= RightBound; i++)
        for (var j = TopBound; j <= BottomBound; j++)
            GetTile(i, j).Value.Room?.Draw(this, new Vector2(i * 112, j * 112));
    }
}