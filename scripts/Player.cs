using CanIDoThis.scripts;
using CanIDoThis.scripts.Components;
using Godot;

public partial class Player : Area2D
{
    [Export] private float MovementSpeed { get; set; } = 25.0f;
    [Export] private Cannon Cannon { get; set; }
    
    private void OnUnitHit(Area2D collidedWith)
    {
        if (collidedWith is not Projectile projectile)
        {
            return;
        }
        
        projectile.CollisionOccured();
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

        if (Input.IsActionJustPressed("shoot"))
        {
            Cannon.Fire();
        }
    }
}