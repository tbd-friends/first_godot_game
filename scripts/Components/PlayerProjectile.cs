using Godot;

namespace CanIDoThis.scripts.Components;

public partial class PlayerProjectile : Projectile
{
    public override void _PhysicsProcess(double delta)
    {
        Position += FiringVector * Speed * (float)delta;
    }
}