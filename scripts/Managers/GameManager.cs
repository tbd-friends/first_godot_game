using System.Linq;
using Godot;

namespace CanIDoThis.scripts;

public partial class GameManager : Node
{
    [Export] private TileMap WorldMap;
    [Export] private Camera Camera;
    [Export] private ScoreKeeper ScoreKeeper;

    [Export] private PackedScene GameOverScene;

    private bool _isCameraStopped;

    public override void _EnterTree()
    {
        Camera.OnCameraStopped += OnCameraStopped;

        base._EnterTree();
    }

    public override void _Ready()
    {
        Player player = GetTree()
            .GetNodesInGroup("Players")
            .OfType<Player>()
            .Single();

        player.DeathOccurred += OnPlayerDeath;
    }

    private void OnPlayerDeath(Player player)
    {
        _isCameraStopped = true;

        var gameOverScene = GameOverScene.Instantiate();

        AddChild(gameOverScene);
    }

    public override void _Process(double delta)
    {
        if (!_isCameraStopped)
        {
            Camera.Position += Vector2.Up * Camera.MovementSpeed * (float)delta;
        }
    }

    public override void _ExitTree()
    {
        Camera.OnCameraStopped -= OnCameraStopped;

        base._ExitTree();
    }

    private void OnCameraStopped()
    {
        _isCameraStopped = true;
    }
}