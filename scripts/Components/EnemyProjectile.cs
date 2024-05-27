using Godot;

namespace CanIDoThis.scripts.Components;

public partial class EnemyProjectile : Projectile
{
    private Player _target;

    public override void _Ready()
    {
        base._Ready();

        _target = (GetTree().CurrentScene as GameManager)?.Player;

        if (_target is null)
        {
            GD.Print("Player not found!");

            return;
        }

        FiringVector = GlobalPosition.DirectionTo(_target.GlobalPosition);
    }

    public override void SetOrigin<TWeapon>(TWeapon weapon)
    {
        GlobalPosition = weapon.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += FiringVector * Speed * (float)delta;
    }
}