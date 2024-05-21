using Godot;

namespace CanIDoThis.scripts.Components;

public partial class EnemyProjectile : Projectile
{
    public override void SetOrigin<TWeapon>(TWeapon weapon)
    {
        Position = weapon.Position;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Down * Speed * (float)delta;
    }
}