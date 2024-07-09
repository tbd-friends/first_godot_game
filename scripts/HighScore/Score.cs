using Godot;

namespace CanIDoThis.scripts.HighScore;

public partial class Score : HBoxContainer
{
    [Export] private Label PlayerInitials { get; set; }
    [Export] private Label ScoreValue { get; set; }

    public void Initialize(ScoreRecord record) 
    {
        PlayerInitials.Text = record.Player;
        ScoreValue.Text = $"{record.Score:00000}";
    }
}