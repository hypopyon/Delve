using Godot;
using System;

public partial class Player : Node2D
{
    public override void _Process(double delta) {
        var moveUp = Input.IsActionPressed("move_up");
        var moveDown = Input.IsActionPressed("move_down");
        var moveRight = Input.IsActionPressed("move_right");
        var moveLeft = Input.IsActionPressed("move_left");
        var moveVector = Vector2.Zero;
        if (moveRight && !moveLeft) {
            moveVector.x = 1;
        } else if (moveLeft && !moveRight) {
            moveVector.x = -1;
        }
        if (moveDown && !moveUp) {
            moveVector.y = 1;
        } else if (moveUp && !moveDown) {
            moveVector.y = -1;
        }

        Position += moveVector;
    }
}
