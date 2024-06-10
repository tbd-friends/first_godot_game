using CanIDoThis.scripts.Components;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class Cannon : Node2D, IWeapon
{
    [Export] private PackedScene _bulletScene;

    private ProjectileManager _projectileManager;

    public override void _Ready()
    {
        _projectileManager = (GetTree().CurrentScene as GameManager)?.ProjectileManager;
    }

    public void Fire()
    {
        Projectile bullet = _bulletScene.Instantiate<Projectile>();

        _projectileManager.AddChild(bullet);

        bullet.SetOrigin(this);
    }

    public void FireAt(Vector2 target)
    {
        Projectile bullet = _bulletScene.Instantiate<Projectile>();

        _projectileManager.AddChild(bullet);

        bullet.SetOrigin(this);

        bullet.FireAt(target);
    }
}