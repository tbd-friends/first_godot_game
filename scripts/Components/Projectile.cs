using Godot;

namespace CanIDoThis.scripts.Components;

public abstract partial class Projectile : Area2D
{
    [Export] public float Damage { get; set; } = 10f;
    [Export] protected float Speed { get; set; } = 120f;
    [Export] protected VisibleOnScreenNotifier2D Notifier { get; set; }

    protected Vector2 FiringVector = Vector2.Up;

    public override void _Ready()
    {
        Notifier.ScreenExited += ProjectileExitedScreen;
    }

    public virtual void SetOrigin<TWeapon>(TWeapon weapon)
        where TWeapon : Node2D
    {
        GlobalPosition = weapon.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {   
        Position += FiringVector * Speed * (float)delta;
    }

    public void CollisionOccured()
    {
        QueueFree();
    }

    private void ProjectileExitedScreen()
    {
        Notifier.ScreenExited -= ProjectileExitedScreen;

        QueueFree();
    }
}