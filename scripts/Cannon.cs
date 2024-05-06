using Godot;

namespace CanIDoThis.scripts;

public partial class Cannon : Node
{
    [Export] PackedScene BulletScene;

    private Node2D _bulletSpawn;

    public override void _Ready()
    {
        _bulletSpawn = GetNode<Node2D>("Spawn");
    }

    public void Fire()
    {
        Bullet bullet = BulletScene.Instantiate<Bullet>();

        bullet.GlobalPosition = _bulletSpawn.GlobalPosition;

        AddChild(bullet);
    }
}