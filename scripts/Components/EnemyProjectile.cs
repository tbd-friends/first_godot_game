using System;
using Godot;

namespace CanIDoThis.scripts.Components;

public partial class EnemyProjectile : Projectile
{
    public override void SetOrigin<TWeapon>(TWeapon weapon)
    {
        GlobalPosition = weapon.GlobalPosition;

        Rotate(Single.Pi / 2);
    }

    public override void FireAt(Vector2 target)
    {
        FiringVector = GlobalPosition.DirectionTo(target);

        LookAt(target);
    }
}