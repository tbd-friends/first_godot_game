using CanIDoThis.scripts;
using Godot;

public partial class Scoring : Node
{
    [Export] private ScoreKeeper ScoreKeeper;
    [Export] private Label ScoreLabel;

    public override void _Ready()
    {
        ScoreKeeper.ScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        ScoreLabel.Text = $"{score}";
    }

    private void __ExitTree()
    {
        ScoreKeeper.ScoreChanged -= OnScoreChanged;
    }
}