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

        GD.Print($"{Name} {FiringVector}");
    }


    public override void _Draw()
    {
        DrawLine(GlobalPosition, _target.GlobalPosition, Colors.Red, 2f);
    }
}