using Godot;
using System;
using Delve;

namespace Delve;

public partial class Player : Node2D {

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

        if (moveVector != Vector2.Zero)
            Position += moveVector;
    }
}
