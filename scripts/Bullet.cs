using CanIDoThis.scripts;
using CanIDoThis.scripts.Contracts;
using Godot;

public partial class Bullet : Area2D
{
    public float Speed { get; set; } = 120f;

    private VisibleOnScreenNotifier2D _notifier;

    public void SetOrigin<TWeapon>(TWeapon cannon)
        where TWeapon : Node2D, IWeapon
    {
        GlobalPosition = cannon.GlobalPosition;
    }

    public override void _Ready()
    {
        _notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");

        _notifier.ScreenExited += BulletExitedScreen;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Up * Speed * (float)delta;
    }

    public void CollisionOccured()
    {
        QueueFree();
    }

    private void BulletExitedScreen()
    {
        QueueFree();
    }

    public override void _ExitTree()
    {
        _notifier.ScreenExited -= BulletExitedScreen;
        _notifier = null;

        base._ExitTree();
    }
}