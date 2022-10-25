using System;
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
        var relativeWorldMousePos = worldMousePos - GlobalPosition;
        
        var nearestTileX = Mathf.Round(relativeWorldMousePos.x / SpacedTileWidth);
        if (nearestTileX >= LeftTileBound && nearestTileX <= RightTileBound)
            hoverTileX = Convert.ToInt32(nearestTileX);
        else hoverTileX = null;
        var nearestTileY = MathF.Round(relativeWorldMousePos.y / SpacedTileHeight);
        
        if (nearestTileY >= TopTileBound && nearestTileY <= BottomTileBound + 1)
            hoverTileY = Convert.ToUInt32(nearestTileY);
        else hoverTileY = null;

        if (hoverTileX is not null && hoverTileY is not null) {
            var selectWorldPosition = new Vector2(nearestTileX * SpacedTileWidth, nearestTileY * SpacedTileHeight);
            var angle = selectWorldPosition.AngleToPoint(relativeWorldMousePos);
            var dir = (Mathf.RoundToInt(angle / (Mathf.Pi / 2) + 2) % 4) switch {
                0 => Direction.Right,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Up,
                _ => throw new ArithmeticException()
            };
            
            switch (dir) {
                case Direction.Right:
                    if (hoverTileX.Value < RightTileBound)
                        hoverAdjacentTileX = hoverTileX.Value + 1;
                    else hoverAdjacentTileX = null;
                    hoverAdjacentTileY = hoverTileY.Value;
                    break;
                case Direction.Up:
                    hoverAdjacentTileX = hoverTileX.Value;
                    if (hoverTileY.Value > TopTileBound)
                        hoverAdjacentTileY = hoverTileY.Value - 1;
                    else hoverAdjacentTileY = null;
                    break;
                case Direction.Left:
                    if (hoverTileX.Value > LeftTileBound)
                        hoverAdjacentTileX = hoverTileX.Value - 1;
                    else hoverAdjacentTileX = null;
                    hoverAdjacentTileY = hoverTileY.Value;
                    break;
                case Direction.Down:
                    hoverAdjacentTileX = hoverTileX.Value;
                    if (hoverTileY.Value < BottomTileBound)
                        hoverAdjacentTileY = hoverTileY.Value + 1;
                    else hoverAdjacentTileY = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}