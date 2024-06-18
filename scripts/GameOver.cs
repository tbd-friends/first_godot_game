using Godot;

namespace CanIDoThis.scripts;

public partial class GameOver : Node
{
    [Export] private ScoreKeeper ScoreKeeper { get; set; }

    private RichTextLabel _scoreLabel;

    public override void _Ready()
    {
        ScoreKeeper = GetTree().CurrentScene.GetNode<ScoreKeeper>("ScoreKeeper");
        _scoreLabel = GetNode<RichTextLabel>("%ScoreLabel");
    }

    public override void _Process(double delta)
    {
        _scoreLabel.Text = $"[center]{ScoreKeeper.Score} pts[/center]";

        if (Input.IsActionPressed("quit"))
        {
            GetTree().Quit();
        }
    }
}