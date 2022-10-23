using Godot;
using System;
using Delve;

namespace Delve;

public partial class CameraController : Node2D {
    Camera2D camera = null!;
    
    public override void _Ready() {
        if (GetNode("Camera2D") is Camera2D getCamera)
            camera = getCamera;
        else throw new Exception();
    }
    
    
    public override void _Process(double delta) {
        var moveUp = Input.IsActionPressed(Actions.MoveUp);
        var moveDown = Input.IsActionPressed(Actions.MoveDown);
        var moveRight = Input.IsActionPressed(Actions.MoveRight);
        var moveLeft = Input.IsActionPressed(Actions.MoveLeft);
        
        
        var moveVector = Vector2.Zero;
        if (moveRight && !moveLeft)
            moveVector.x = 1;
        else if (moveLeft && !moveRight)
            moveVector.x = -1;
        if (moveDown && !moveUp)
            moveVector.y = 1;
        else if (moveUp && !moveDown)
            moveVector.y = -1;
        moveVector = moveVector.Normalized() / camera.Zoom * 2;
        if (moveVector != Vector2.Zero)
            Position += moveVector;

        var zoomIn = Input.IsActionPressed(Actions.ZoomIn);
        var zoomOut = Input.IsActionPressed(Actions.ZoomOut);

        if (zoomIn && !zoomOut)
            camera.Zoom = new Vector2(
                Mathf.Min(3f, camera.Zoom.x + 0.005f),
                Mathf.Min(3f, camera.Zoom.y + 0.005f)
            );
        if (zoomOut && !zoomIn)
            camera.Zoom = new Vector2(
                Mathf.Max(0.25f, camera.Zoom.x - 0.005f),
                Mathf.Max(0.25f, camera.Zoom.y - 0.005f)
            );
    }
}
