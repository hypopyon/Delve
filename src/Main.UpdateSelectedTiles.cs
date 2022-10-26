using System;
using Godot;

namespace Delve; 

public partial class Main {
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
        
        int? hoverTileX = null, hoverAdjacentTileX = null;
        uint? hoverTileY = null, hoverAdjacentTileY = null;
        var nearestTileX = Mathf.Round(relativeWorldMousePos.x / Textures.SpacedTileWidth);
        if (nearestTileX >= GameMap.LeftTileBound && nearestTileX <= GameMap.RightTileBound)
            hoverTileX = Convert.ToInt32(nearestTileX);
        else hoverTileX = null;
        var nearestTileY = MathF.Round(relativeWorldMousePos.y / Textures.SpacedTileHeight);
        if (nearestTileY >= GameMap.TopTileBound && nearestTileY <= Map.BottomTileBound)
            hoverTileY = Convert.ToUInt32(nearestTileY);
        else hoverTileY = null;

        if (hoverTileX is not null && hoverTileY is not null) {
            var selectWorldPosition = new Vector2(nearestTileX * Textures.SpacedTileWidth, nearestTileY * Textures.SpacedTileHeight);
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
                    hoverAdjacentTileX = hoverTileX.Value + 1;
                    hoverAdjacentTileY = hoverTileY.Value;
                    break;
                case Direction.Up:
                    hoverAdjacentTileX = hoverTileX.Value;
                    hoverAdjacentTileY = hoverTileY.Value - 1;
                    break;
                case Direction.Left:
                    hoverAdjacentTileX = hoverTileX.Value - 1;
                    hoverAdjacentTileY = hoverTileY.Value;
                    break;
                case Direction.Down:
                    hoverAdjacentTileX = hoverTileX.Value;
                    hoverAdjacentTileY = hoverTileY.Value + 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (hoverTileX is not null && hoverTileY is not null) {
            var getHoverTile = Map.GetTile(hoverTileX.Value, hoverTileY.Value);
            hoverTile = getHoverTile.IsSuccessful ? getHoverTile.Value : null;
        } else hoverTile = null;

        if (hoverAdjacentTileX is not null && hoverAdjacentTileY is not null) {
            var getHoverAdjacentTile = Map.GetTile(hoverAdjacentTileX.Value, hoverAdjacentTileY.Value);
            hoverAdjacentTile = getHoverAdjacentTile.IsSuccessful ? getHoverAdjacentTile.Value : null;
        } else hoverAdjacentTile = null;

    }
}