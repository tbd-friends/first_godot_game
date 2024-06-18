using Godot;

namespace CanIDoThis.scripts;

public partial class Radar : Node
{
    private GameManager _gameManager;

    public override void _Ready()
    {
        _gameManager = GetTree().CurrentScene as GameManager;
    }

    public Vector2 FetchTarget()
    {
        return _gameManager.Player.GlobalPosition;
    }
}