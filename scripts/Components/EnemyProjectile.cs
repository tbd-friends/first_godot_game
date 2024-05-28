using System;
using Godot;

namespace CanIDoThis.scripts.Components;

public partial class EnemyProjectile : Projectile
{
    public override void SetOrigin<TWeapon>(TWeapon weapon)
    {
        GlobalPosition = weapon.GlobalPosition;

        var target = (GetTree().CurrentScene as GameManager)?.Player;

        if (target is null)
        {
            GD.Print("Player not found!");

            return;
        }

        FiringVector = GlobalPosition.DirectionTo(target.GlobalPosition);

        LookAt(target.GlobalPosition);
        Rotate(Single.Pi / 2);
    }
}