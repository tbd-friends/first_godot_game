using System;
using Godot;

namespace CanIDoThis.scripts.Enemies;

public partial class Enemy : PlayerTrackingEnemy
{
    private Vector2 _currentTarget;

    public override void _Process(double delta)
    {
        if (IsDead)
        {
            return;
        }

        _currentTarget = Radar.FetchTarget();

        LookAt(_currentTarget);

        Rotate(-Single.Pi / 2);
    }

    protected override void OnFiringTimerTimeout()
    {
        Cannon.FireAt(_currentTarget);
    }
}