using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts.Components;

public partial class Projectile : Area2D
{
    [Export] private float Speed { get; set; } = 120f;

    [Export] private VisibleOnScreenNotifier2D _notifier;

    public override void _Ready()
    {
        _notifier.ScreenExited += ProjectileExitedScreen;
    }

    public void SetOrigin<TWeapon>(TWeapon weapon)
        where TWeapon : Node2D, IWeapon
    {
        GlobalPosition = weapon.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Up * Speed * (float)delta;
    }

    public void CollisionOccured()
    {
        QueueFree();
    }

    private void ProjectileExitedScreen()
    {
        QueueFree();
    }

    public override void _ExitTree()
    {
        _notifier.ScreenExited -= ProjectileExitedScreen;
        _notifier = null;

        base._ExitTree();
    }
}