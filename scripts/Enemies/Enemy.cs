using System;
using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

public partial class Enemy : Area2D, IScoreable
{
    [Export] private Timer _endingTimer;
    [Export] private Sprite2D _explosion;
    public event Action<IScoreable> CollisionOccured;
    
    public override void _Ready()
    {
        _endingTimer.Timeout += QueueFree;
    }

    private void OnUnitHit(Area2D collidedWith)
    {
        if (collidedWith is not Projectile bullet)
        {
            return;
        }

        bullet.CollisionOccured();

        CollisionOccured?.Invoke(this);

        _endingTimer.Start();
        _explosion.Visible = true;
    }

    private void __ExitTree()
    {
        _endingTimer.Timeout -= QueueFree;
        _endingTimer.QueueFree();

        _endingTimer = null;
    }

    [Export] public int Score { get; set; }
}