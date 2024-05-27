using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class Cannon : Node2D, IWeapon
{
    [Export] private PackedScene _bulletScene;

    public void Fire()
    {
        Projectile bullet = _bulletScene.Instantiate<Projectile>();

        bullet.Visible = false;

        AddChild(bullet);
        
        bullet.SetOrigin(this);

        bullet.Visible = true;
    }
}