using System;
using System.Numerics;
using CanIDoThis.scripts;
using CanIDoThis.scripts.Components;
using Godot;
using Vector2 = Godot.Vector2;

public partial class Player : Area2D
{
    [Export] private float MovementSpeed { get; set; } = 25.0f;
    [Export] private Cannon Cannon { get; set; }
    [Export] private float Health { get; set; } = 100.0f;

    public event Action<Player> DeathOccurred;

    private void OnUnitHit(Area2D collidedWith)
    {
        if (collidedWith is not Projectile projectile)
        {
            return;
        }

        projectile.CollisionOccured();

        Health -= projectile.Damage;

        if (Health > 0)
        {
            return;
        }

        DeathOccurred?.Invoke(this);

        QueueFree();
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