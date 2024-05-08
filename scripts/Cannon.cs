using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class Cannon : Node2D, IWeapon
{
    [Export] private PackedScene BulletScene;

    public void Fire()
    {
        Projectile bullet = BulletScene.Instantiate<Projectile>();

        bullet.SetOrigin(this);

        AddChild(bullet);
    }
}