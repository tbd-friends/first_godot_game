using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts.Components;

public abstract partial class Projectile : Area2D
{
    [Export] public float Damage { get; set; } = 10f;
    [Export] protected float Speed { get; set; } = 120f;

    [Export] protected VisibleOnScreenNotifier2D _notifier;

    public override void _Ready()
    {
        _notifier.ScreenExited += ProjectileExitedScreen;
    }

    public abstract void SetOrigin<TWeapon>(TWeapon weapon)
        where TWeapon : Node2D, IWeapon;

    public void CollisionOccured()
    {
        QueueFree();
    }

    private void ProjectileExitedScreen()
    {
        _notifier.ScreenExited -= ProjectileExitedScreen;

        QueueFree();
    }
}