using CanIDoThis.scripts.Contracts;
using Godot;
using Vector2 = Godot.Vector2;

namespace CanIDoThis.scripts.Enemies;

public partial class Aircraft : EnemyBase, IWaveTriggered
{
    [Export] private float _speed = 110f;

    public override void _Ready()
    {
        base._Ready();

        IsActive = false;
    }

    protected override void OnFiringTimerTimeout()
    {
        Cannon.FireAt(Vector2.Down);
    }

    public override void _Process(double delta)
    {
        if (!IsActive)
        {
            return;
        }

        Position += Vector2.Down * _speed * (float)delta;
    }

    public void NotifyWaveTriggered()
    {
        FiringTimer.Start();

        IsActive = true;
    }
}