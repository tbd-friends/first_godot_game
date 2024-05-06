using Godot;

public partial class Bullet : Area2D
{
    public float Speed { get; set; } = 120f;

    private VisibleOnScreenNotifier2D _notifier;

    public override void _Ready()
    {
        _notifier = GetNode<VisibleOnScreenNotifier2D>("Notifier");

        _notifier.ScreenExited += BulletExitedScreen;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Up * Speed * (float)delta;
    }

    private void BulletExitedScreen()
    {
        QueueFree();
    }

    public override void _ExitTree()
    {
        _notifier.ScreenExited -= BulletExitedScreen;

        base._ExitTree();
    }
}