using System;
using CanIDoThis.scripts.Contracts;
using Godot;

public partial class Enemy : Area2D, IScoreable
{
    [Export] private Timer _endingTimer;
    public event Action<IScoreable> CollisionOccured;
    private Sprite2D _explosion;

    public override void _Ready()
    {
        _endingTimer.Timeout += QueueFree;
        _explosion = GetNode<Sprite2D>("Explosion");
    }

    private void OnUnitHit(Area2D collidedWith)
    {
        if (collidedWith is not Bullet bullet)
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