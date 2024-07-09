using System;
using Godot;

namespace CanIDoThis.scripts;

public partial class GameOver : Node
{
    public event Action OnRestartGame;
    public event Action OnEndOfGame;
    [Export] private ScoreKeeper ScoreKeeper { get; set; }

    private RichTextLabel _scoreLabel;

    public override void _Ready()
    {
        ScoreKeeper = GetTree().CurrentScene.GetNode<ScoreKeeper>("ScoreKeeper");
        
        _scoreLabel = GetNode<RichTextLabel>("%FinalScoreLabel");
    }

    public override void _Process(double delta)
    {
        _scoreLabel.Text = $"[center]{ScoreKeeper.Score} pts[/center]";

        if (Input.IsActionPressed("quit"))
        {
            OnEndOfGame?.Invoke();

            GetTree().Quit();
        }

        if (Input.IsActionPressed("shoot"))
        {
            OnRestartGame?.Invoke();
        }
    }
}