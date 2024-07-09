using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;

namespace CanIDoThis.scripts.HighScore;

public partial class HighScore : Node
{
    [Export] private PackedScene ScoreComponent { get; set; }

    private const string SaveFilePath = "user://high_scores.json";

    private static IEnumerable<ScoreRecord> DefaultScores = new[]
    {
        new ScoreRecord("AAA", 10000),
        new ScoreRecord("BBB", 9000),
        new ScoreRecord("CCC", 800),
        new ScoreRecord("DDD", 700),
        new ScoreRecord("EEE", 600),
        new ScoreRecord("FFF", 500),
        new ScoreRecord("GGG", 400),
        new ScoreRecord("HHH", 300),
        new ScoreRecord("III", 200),
        new ScoreRecord("JJJ", 100)
    };

    public override void _Ready()
    {
        var scores = GetTop10Scores();

        foreach (var score in scores)
        {
            var scoreComponent = ScoreComponent.Instantiate() as Score;

            scoreComponent!.Initialize(score);

            AddChild(scoreComponent);
        }
    }

    private IEnumerable<ScoreRecord> GetTop10Scores()
    {
        var saveFile = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Read);

        if (saveFile is null)
        {
            saveFile = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.WriteRead);

            saveFile.StoreString(JsonSerializer.Serialize(DefaultScores));

            return DefaultScores;
        }

        var content = saveFile.GetAsText();

        return !string.IsNullOrEmpty(content) ? JsonSerializer.Deserialize<List<ScoreRecord>>(content) : DefaultScores;
    }

    public void RecordScore(ScoreRecord record)
    {
        var saveFile = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.ReadWrite);

        var content = saveFile.GetAsText();

        var scores =
            !string.IsNullOrEmpty(content) ? JsonSerializer.Deserialize<List<ScoreRecord>>(content) : [];

        scores.Add(record);

        var top10 = scores.OrderBy(s => s.Score).Take(10);

        saveFile.StoreString(JsonSerializer.Serialize(top10));
    }
}

public record ScoreRecord(string Player, int Score);