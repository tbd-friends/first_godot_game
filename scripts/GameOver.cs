using Godot;

namespace CanIDoThis.scripts;

public partial class GameOver : Node
{
    [Export] private ScoreKeeper ScoreKeeper { get; set; }

    private RichTextLabel _scoreLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScoreKeeper = GetTree().CurrentScene.GetNode<ScoreKeeper>("ScoreKeeper");
        _scoreLabel = GetNode<RichTextLabel>("%ScoreLabel");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _scoreLabel.Text = $"[center]{ScoreKeeper.Score} pts[/center]";
    }
}