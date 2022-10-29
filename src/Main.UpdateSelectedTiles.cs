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
        
        var nearestTileX = Mathf.Round(relativeWorldMousePos.x / Textures.SpacedTileWidth);
        int? hoverTileX = nearestTileX is >= GameMap.LeftmostTile and <= GameMap.RightmostTile
            ? Convert.ToInt32(nearestTileX) : null;
        
        var nearestTileY = MathF.Round(relativeWorldMousePos.y / Textures.SpacedTileHeight);
        int? hoverTileY = nearestTileY >= GameMap.TopmostTile && nearestTileY <= Map.BottommostTile
            ? Convert.ToInt32(nearestTileY) : null;

        if (hoverTileX is not null && hoverTileY is not null) {
            var getHoverTile = Map.GetTile(new Vector2i(hoverTileX.Value, hoverTileY.Value));
            hoverTile = getHoverTile.IsSuccessful ? getHoverTile.Value : null;
        } else hoverTile = null;

        int? hoverAdjacentTileX;
        int? hoverAdjacentTileY;
        
        if (hoverTile is not null) {
            var selectWorldPosition = new Vector2(nearestTileX * Textures.SpacedTileWidth, nearestTileY * Textures.SpacedTileHeight);
            var angle = selectWorldPosition.AngleToPoint(relativeWorldMousePos);
            var dir = (Mathf.RoundToInt(angle / (Mathf.Pi / 2) + 2) % 4) switch {
                0 => Direction.Right,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Up,
                _ => throw new ArithmeticException()
            };

            (hoverAdjacentTileX, hoverAdjacentTileY) = dir switch {
                Direction.Right => hoverTileX + 1 <= GameMap.RightmostTile ?
                    (hoverTile.X + 1, hoverTile.Y) : ((int?)null, (int?)null),
                Direction.Up => hoverTileY - 1 >= GameMap.TopmostTile ?
                    (hoverTile.X, hoverTile.Y - 1) : (null, null),
                Direction.Left => hoverTileX - 1 >= GameMap.LeftmostTile ?
                    (hoverTile.X - 1, hoverTile.Y) : (null, null),
                Direction.Down => hoverTileY + 1 <= Map.BottommostTile ?
                    (hoverTile.X, hoverTile.Y + 1) : (null, null),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (hoverAdjacentTileX is not null && hoverAdjacentTileY is not null) {
                var getHoverAdjacentTile = Map.GetTile(
                    new Vector2i(hoverAdjacentTileX.Value, hoverAdjacentTileY.Value)
                    );
                hoverAdjacentTile = getHoverAdjacentTile.IsSuccessful ? getHoverAdjacentTile.Value : null;
            } else hoverAdjacentTile = null;
        } else hoverAdjacentTile = null;
    }
}