using System;
using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts.Enemies;

public partial class Enemy : Area2D, IEnemy
{
    [Export] private Timer _endingTimer;
    [Export] private Timer _firingTimer;
    [Export] private Sprite2D _explosion;
    [Export] private Cannon _cannon;
    [Export] private VisibleOnScreenNotifier2D _notifier;
    [Export] public Radar Radar { get; set; }
    [Export] public int ScoreValue { get; set; }
    public event Action<IScoreable> CollisionOccured;

    private Vector2 _currentTarget;
    private bool _isDead = false;

    public override void _Ready()
    {
        _endingTimer.Timeout += Shutdown;
        _firingTimer.Timeout += OnFiringTimerTimeout;

        _notifier.ScreenEntered += OnVisibilityChanged;
        _notifier.ScreenExited += OnVisibilityChanged;
    }

    public override void _Process(double delta)
    {
        _currentTarget = Radar.FetchTarget();

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
        if (_isDead)
        {
            return;
        }

        if (collidedWith is not Projectile projectile)
        {
            return;
        }

        projectile.CollisionOccured();

        CollisionOccured?.Invoke(this);
        
        _explosion.Visible = true;

        _firingTimer.Stop();
        _endingTimer.Start();
        
        _isDead = true;
    }

    private void Shutdown()
    {
        _notifier.ScreenEntered -= OnVisibilityChanged;
        _notifier.ScreenExited -= OnVisibilityChanged;
        _endingTimer.Timeout -= Shutdown;
        _firingTimer.Timeout -= OnFiringTimerTimeout;

        QueueFree();
    }

    public void NotifyOnGameOver()
    {
        Shutdown();
    }
}