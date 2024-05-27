using System;
using CanIDoThis.scripts;
using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

public partial class Enemy : Area2D, IScoreable
{
    [Export] private Timer _endingTimer;
    [Export] private Timer _firingTimer;
    [Export] private Sprite2D _explosion;
    [Export] private Cannon _cannon;
    [Export] private VisibleOnScreenNotifier2D _notifier;

    public event Action<IScoreable> CollisionOccured;

    public override void _Ready()
    {
        _endingTimer.Timeout += QueueFree;
        _firingTimer.Timeout += OnFiringTimerTimeout;

        _notifier.ScreenEntered += OnVisibilityChanged;
        _notifier.ScreenExited += OnVisibilityChanged;
    }

    private void OnFiringTimerTimeout()
    {
        _cannon.Fire();
    }

    private void OnVisibilityChanged()
    {
        if (_notifier.IsOnScreen())
        {
            _firingTimer.Start();
        }
        else
        {
            _firingTimer.Stop();
        }
    }

    private void OnUnitHit(Area2D collidedWith)
    {
        if (collidedWith is not Projectile projectile)
        {
            return;
        }

        projectile.CollisionOccured();

        CollisionOccured?.Invoke(this);

        _endingTimer.Start();
        _explosion.Visible = true;
    }

    private void __ExitTree()
    {
        _endingTimer.Timeout -= QueueFree;
        _firingTimer.Timeout -= OnFiringTimerTimeout;

        _endingTimer.QueueFree();
        _firingTimer.QueueFree();

        _notifier.VisibilityChanged -= OnVisibilityChanged;

        _endingTimer = null;
        _notifier = null;
    }

    [Export] public int Score { get; set; }
}