using Godot;

public partial class Player : CharacterBody2D
{
    [Export] private float MovementSpeed { get; set; } = 25.0f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

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
    }
}