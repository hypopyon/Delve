using System;
using Delve.Rooms;
using Godot;



namespace Delve;
public partial class Map : Node2D {
    public override void _Process(double delta) {

        var mousePos = GetGlobalMousePosition();
        var mouseTileX = Mathf.Round(mousePos.x / 112);
        if (mouseTileX >= LeftBound && mouseTileX <= RightBound)
            selectX = Convert.ToInt32(mouseTileX);
        else selectX = null;
        var mouseTileY = MathF.Round(mousePos.y / 112);
        if (mouseTileY >= TopBound && mouseTileY <= BottomBound + 1)
            selectY = Convert.ToUInt32(mouseTileY);
        else selectY = null;

        if (selectX is not null && selectY is not null) {
            var selectWorldPosition = new Vector2(Convert.ToSingle(selectX) * 112, Convert.ToSingle(selectY) * 112);
            var angle = selectWorldPosition.AngleToPoint(GetGlobalMousePosition());
            Direction dir = (Mathf.Round(angle / (Mathf.Pi / 2) + 2) % 4) switch {
                0 => Direction.Right,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Up,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            switch (dir) {
                case Direction.Right:
                    if (selectX.Value < RightBound)
                        selectAdjacentX = selectX.Value + 1;
                    else selectAdjacentX = null;
                    selectAdjacentY = selectY.Value;
                    break;
                case Direction.Up:
                    selectAdjacentX = selectX.Value;
                    if (selectY.Value > TopBound)
                        selectAdjacentY = selectY.Value - 1;
                    else selectAdjacentY = null;
                    break;
                case Direction.Left:
                    if (selectX.Value > LeftBound)
                        selectAdjacentX = selectX.Value - 1;
                    else selectAdjacentX = null;
                    selectAdjacentY = selectY.Value;
                    break;
                case Direction.Down:
                    selectAdjacentX = selectX.Value;
                    if (selectY.Value < BottomBound)
                        selectAdjacentY = selectY.Value + 1;
                    else selectAdjacentY = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Input.IsActionJustPressed("interact_main")) {
                if (selectY > BottomBound)
                    ExpandDownwards(Convert.ToUInt32(selectY - BottomBound));
                var tile = GetTile(Convert.ToInt32(selectX), Convert.ToUInt32(selectY));
                if (tile.Value.Room == null)
                    tile.Value.Room = new Cavern();
            }
        }

        QueueRedraw();
    }
}