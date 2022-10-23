using System;
using Delve.Rooms;
using Godot;

namespace Delve;

public partial class Map : Node2D {
    void UpdateSelectedTiles() {
        var viewport = GetViewport();
        var camera = viewport.GetCamera2d();
        var visibleRect = viewport.GetVisibleRect();
        var worldMousePos = camera.AnchorMode switch {
            Camera2D.AnchorModeEnum.FixedTopLeft => viewport.GetMousePosition(),
            Camera2D.AnchorModeEnum.DragCenter => viewport.GetMousePosition() - visibleRect.Size / 2,
            _ => throw new ArgumentOutOfRangeException()
        } / camera.Zoom + camera.GlobalPosition;
        var relativeWorldMousePos = worldMousePos - Position;
        
        var nearestTileX = Mathf.Round(relativeWorldMousePos.x / SpacedTileWidth);
        if (nearestTileX >= LeftBound && nearestTileX <= RightBound)
            selectX = Convert.ToInt32(nearestTileX);
        else selectX = null;
        var nearestTileY = MathF.Round(relativeWorldMousePos.y / SpacedTileHeight);
        
        if (nearestTileY >= TopBound && nearestTileY <= BottomBound + 1)
            selectY = Convert.ToUInt32(nearestTileY);
        else selectY = null;

        if (selectX is not null && selectY is not null) {
            var selectWorldPosition = new Vector2(nearestTileX * SpacedTileWidth, nearestTileY * SpacedTileHeight);
            var angle = selectWorldPosition.AngleToPoint(relativeWorldMousePos);
            var dir = (Mathf.Round(angle / (Mathf.Pi / 2) + 2) % 4) switch {
                0 => Direction.Right,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Up,
                _ => throw new ArithmeticException()
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

            
        }
    }
}