using Godot;
using System;
using CanIDoThis.scripts;
using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Vector2 = Godot.Vector2;

public partial class Aircraft : Area2D, IEnemy, IWaveTriggered
{
    [Export] private Timer _endingTimer;
    [Export] private Timer _firingTimer;
    [Export] private Sprite2D _explosion;
    [Export] private Cannon _cannon;
    [Export] private float _speed = 100f;

    public event Action<IScoreable> CollisionOccured;
    public int ScoreValue { get; set; }
    public Radar Radar { get; set; }

    private bool _isDead = false;
    private bool _isEnabled = false;

    public override void _Ready()
    {
        _firingTimer.Timeout += OnFiringTimerTimeout;
        _endingTimer.Timeout += Shutdown;
    }

    private void OnFiringTimerTimeout()
    {
        _cannon.FireAt(Vector2.Down);
    }

    public override void _Process(double delta)
    {
        if (!_isEnabled)
        {
            return;
        }

        Position += Vector2.Down * _speed * (float)delta;
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
        // _notifier.ScreenEntered -= OnVisibilityChanged;
        // _notifier.ScreenExited -= OnVisibilityChanged;
        _endingTimer.Timeout -= Shutdown;
        _firingTimer.Timeout -= OnFiringTimerTimeout;

        QueueFree();
    }

    public void NotifyOnGameOver()
    {
        Shutdown();
    }

    public void NotifyWaveTriggered()
    {
        _firingTimer.Start();

        _isEnabled = true;
    }
}