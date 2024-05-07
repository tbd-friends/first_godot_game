using System;
using CanIDoThis.scripts.Contracts;
using Godot;

public partial class Enemy : Area2D, IScoreable
{
    public event Action<IScoreable> CollisionOccured;

    private void OnUnitHit(Area2D collidedWith)
    {
        if (collidedWith is not Bullet bullet)
        {
            return;
        }

        bullet.CollisionOccured();

        CollisionOccured?.Invoke(this);

        QueueFree();
    }

    [Export] public int Score { get; set; }
}