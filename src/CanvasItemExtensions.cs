using System;
using Godot;

namespace Delve; 

public static class CanvasItemExtensions {
    public static void DrawStringPrecise(
        this CanvasItem canvasItem,
        Font font,
        Vector2 pos,
        Vector2 scale,
        string text,
        float angle = 0,
        HorizontalAlignment hAlign = HorizontalAlignment.Left,
        float width = -1F,
        float fontSize = 16,
        int minAdjustedFontSize = 1,
        int maxAdjustedFontSize = 64,
        Color? modulate = null,
        TextServer.JustificationFlag justFlag
            = TextServer.JustificationFlag.Kashida
              | TextServer.JustificationFlag.None
              | TextServer.JustificationFlag.WordBound,
        TextServer.Direction direction = TextServer.Direction.Auto,
        TextServer.Orientation orientation = TextServer.Orientation.Horizontal
    ) {
        var intSize = Math.Max(Mathf.FloorToInt(fontSize), 1);
        if (intSize > 16)
            intSize = Mathf.RoundToInt(Mathf.Pow(2, Mathf.Ceil(Mathf.Log(fontSize) / Mathf.Log(2))));
        intSize = Math.Clamp(intSize, minAdjustedFontSize, maxAdjustedFontSize);
        var transform = Transform2D.Identity
            .Scaled(fontSize / intSize * scale)
            .Rotated(angle)
            .Translated(pos);
        
        canvasItem.DrawSetTransformMatrix(transform);
        canvasItem.DrawString(
            font,
            Vector2.Zero,
            text,
            hAlign,
            width,
            Mathf.RoundToInt(intSize),
            modulate,
            justFlag,
            direction,
            orientation);
        canvasItem.DrawSetTransform(Vector2.Zero);
    }
    
    public static void DrawStringZoomCorrected(
        this CanvasItem canvasItem,
        Font font,
        Vector2 pos,
        string text,
        float angle = 0f,
        HorizontalAlignment hAlign = HorizontalAlignment.Left,
        float width = -1F,
        float fontSize = 16,
        int minAdjustedFontSize = 1,
        int maxAdjustedFontSize = 64,
        Color? modulate = null,
        TextServer.JustificationFlag justFlag
            = TextServer.JustificationFlag.Kashida
              | TextServer.JustificationFlag.None
              | TextServer.JustificationFlag.WordBound,
        TextServer.Direction direction = TextServer.Direction.Auto,
        TextServer.Orientation orientation = TextServer.Orientation.Horizontal
    ) {
        var zoom = canvasItem.GetViewport().GetCamera2d().Zoom;
        float zoomFactor = Mathf.Max(zoom.x, zoom.y);
        canvasItem.DrawStringPrecise(
            font,
            pos,
            Vector2.One / zoomFactor,
            text,
            angle,
            hAlign,
            width,
            fontSize * zoomFactor,
            minAdjustedFontSize,
            maxAdjustedFontSize,
            modulate,
            justFlag,
            direction,
            orientation);
    }
}