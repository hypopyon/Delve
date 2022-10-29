using Godot;
using System;
using Delve;

namespace Delve;

public partial class CameraController : Node2D {
    const float MoveSpeed = 2f;
    const float MinZoom = 0.25f;
    const float MaxZoom = 3f;
    const float ZoomSpeed = 0.01f;
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
        if (moveRight && moveLeft == false)
            moveVector.x = 1;
        else if (moveLeft && moveRight == false)
            moveVector.x = -1;
        if (moveDown && moveUp == false)
            moveVector.y = 1;
        else if (moveUp && moveDown == false)
            moveVector.y = -1;
        moveVector = moveVector.Normalized() / camera.Zoom * MoveSpeed;
        if (moveVector != Vector2.Zero)
            Position += moveVector;

        var zoomIn = Input.IsActionPressed(Actions.ZoomIn);
        var zoomOut = Input.IsActionPressed(Actions.ZoomOut);

        if (zoomIn && !zoomOut)
            camera.Zoom = new Vector2(
                Mathf.Min(MaxZoom, camera.Zoom.x + ZoomSpeed),
                Mathf.Min(MaxZoom, camera.Zoom.y + ZoomSpeed)
            );
        if (zoomOut && !zoomIn)
            camera.Zoom = new Vector2(
                Mathf.Max(MinZoom, camera.Zoom.x - ZoomSpeed),
                Mathf.Max(MinZoom, camera.Zoom.y - ZoomSpeed)
            );
    }
}
