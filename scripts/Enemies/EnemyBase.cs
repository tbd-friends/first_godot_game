using System;
using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts.Enemies;

public abstract partial class EnemyBase : Area2D, IEnemy
{
    [Export] protected Timer EndingTimer;
    [Export] protected Timer FiringTimer;
    [Export] protected Sprite2D Explosion;
    [Export] protected Cannon Cannon;
    [Export] protected VisibleOnScreenNotifier2D Notifier;
    [Export] public int ScoreValue { get; set; }

    protected bool IsDead = false;
    protected bool IsActive = true;

    public event Action<IScoreable> CollisionOccured;

    public override void _Ready()
    {
        EndingTimer.Timeout += Shutdown;
        FiringTimer.Timeout += OnFiringTimerTimeout;

        Notifier.ScreenEntered += OnVisibilityChanged;
        Notifier.ScreenExited += OnVisibilityChanged;
    }
    
    private void OnUnitHit(Area2D collidedWith)
    {
        if (IsDead || !IsActive)
        {
            return;
        }

        if (collidedWith is not Projectile projectile)
        {
            return;
        }

        projectile.CollisionOccured();

        CollisionOccured?.Invoke(this);

        Explosion.Visible = true;

        FiringTimer.Stop();
        EndingTimer.Start();

        IsDead = true;
    }

    protected abstract void OnFiringTimerTimeout();

    protected virtual void OnVisibilityChanged()
    {
        if (Notifier.IsOnScreen())
        {
            FiringTimer.Start();
        }
        else
        {
            FiringTimer.Stop();
        }
    }

    private void Shutdown()
    {
        Notifier.ScreenEntered -= OnVisibilityChanged;
        Notifier.ScreenExited -= OnVisibilityChanged;
        EndingTimer.Timeout -= Shutdown;
        FiringTimer.Timeout -= OnFiringTimerTimeout;

        QueueFree();
    }

    public void NotifyOnGameOver()
    {
        GD.Print($"GameOver Was Called {Name}");
    }
}