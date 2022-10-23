using System;
using Delve.Rooms;
using Godot;



namespace Delve;
public partial class Map : Node2D {
    public override void _Process(double delta) {
        UpdateSelectedTiles();
        QueueRedraw();
        
        
        var moveUp = Input.IsKeyPressed(Key.Up);
        var moveDown = Input.IsKeyPressed(Key.Down);
        var moveRight = Input.IsKeyPressed(Key.Right);
        var moveLeft = Input.IsKeyPressed(Key.Left);
        
        
        var moveVector = Vector2.Zero;
        if (moveRight && !moveLeft)
            moveVector.x = 1;
        else if (moveLeft && !moveRight)
            moveVector.x = -1;
        if (moveDown && !moveUp)
            moveVector.y = 1;
        else if (moveUp && !moveDown)
            moveVector.y = -1;
        moveVector = moveVector.Normalized();
        if (moveVector != Vector2.Zero)
            Position += moveVector;
    }
}