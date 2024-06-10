using System;
using CanIDoThis.scripts;
using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

public partial class Enemy : Area2D, IEnemy
{
    [Export] private Timer _endingTimer;
    [Export] private Timer _firingTimer;
    [Export] private Sprite2D _explosion;
    [Export] private Cannon _cannon;
    [Export] private VisibleOnScreenNotifier2D _notifier;
    [Export] private Radar _radar;
    public event Action<IScoreable> CollisionOccured;

    private Vector2 _currentTarget;

    public override void _Ready()
    {
        _endingTimer.Timeout += QueueFree;
        _firingTimer.Timeout += OnFiringTimerTimeout;

        _notifier.ScreenEntered += OnVisibilityChanged;
        _notifier.ScreenExited += OnVisibilityChanged;
    }

    public override void _Process(double delta)
    {
        _currentTarget = _radar.FetchTarget();

        LookAt(_currentTarget);

        Rotate(-Single.Pi / 2);
    }

    private void OnFiringTimerTimeout()
    {
        _cannon.FireAt(_currentTarget);
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

        _firingTimer.Stop();
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

    public void NotifyOnGameOver()
    {
        QueueFree();
    }
}