using CanIDoThis.scripts;
using Godot;

public partial class Player : CharacterBody2D
{
    [Export] private float MovementSpeed { get; set; } = 25.0f;
    [Export] private Cannon Cannon { get; set; }
    

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("move_left"))
        {
            Position += Vector2.Left * MovementSpeed * (float)delta;
        }

        if (Input.IsActionPressed("move_right"))
        {
            Position += Vector2.Right * MovementSpeed * (float)delta;
        }

        if (Input.IsActionPressed("move_up"))
        {
            Position += Vector2.Up * MovementSpeed * (float)delta;
        }

        if (Input.IsActionPressed("move_down"))
        {
            Position += Vector2.Down * MovementSpeed * (float)delta;
        }

        if (Input.IsActionJustPressed("shoot"))
        {
            Cannon.Fire();
        }
    }
}