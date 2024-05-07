using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class Cannon : Node2D, IWeapon
{
    [Export] private PackedScene BulletScene;

    public void Fire()
    {
        Bullet bullet = BulletScene.Instantiate<Bullet>();

        bullet.SetOrigin(this);

        AddChild(bullet);
    }
}