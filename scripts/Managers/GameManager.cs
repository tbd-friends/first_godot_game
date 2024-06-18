using System.Linq;
using CanIDoThis.scripts.Managers;
using Godot;
using Godot.Collections;

namespace CanIDoThis.scripts;

public partial class GameManager : Node
{
    [Export] private Level CurrentLevel;
    [Export] private Camera Camera;
    [Export] private ScoreKeeper ScoreKeeper;
    [Export] private EnemyManager EnemyManager;
    [Export] private PackedScene GameOverScene;
    [Export] private PackedScene LevelCompleteScene;
    [Export] public Player Player { get; set; }
    [Export] public ProjectileManager ProjectileManager { get; set; }
    [Export] public Array<PackedScene> Levels { get; set; }

    private bool _isCameraStopped;
    private int _currentLevel = 0;

    public override void _EnterTree()
    {
        LoadNextLevel();

        Camera.OnCameraStopped += OnCameraStopped;

        base._EnterTree();
    }

    public override void _Ready()
    {
        Player.DeathOccurred += OnPlayerDeath;
    }

    private void LoadNextLevel()
    {
        if (_currentLevel > Levels.Count - 1 || Levels[_currentLevel].Instantiate() is not Level level)
        {
            return;
        }

        if (CurrentLevel is not null)
        {
            RemoveChild(CurrentLevel);

            CurrentLevel = null;
        }

        GetNode<Camera2D>("ViewPort").AddSibling(level);

        CurrentLevel = level;
        CurrentLevel.EnemyManager = EnemyManager;

        _currentLevel++;

        _isCameraStopped = false;
    }

    private void OnPlayerDeath(Player player)
    {
        _isCameraStopped = true;

        var gameOverScene = GameOverScene.Instantiate();

        AddChild(gameOverScene);

        EnemyManager.GameOver();
    }

    public override void _Process(double delta)
    {
        if (!_isCameraStopped)
        {
            CurrentLevel.Map.Position += Vector2.Down * Camera.MovementSpeed * (float)delta;
        }
    }

    public override void _ExitTree()
    {
        Camera.OnCameraStopped -= OnCameraStopped;

        base._ExitTree();
    }

    private void OnCameraStopped(bool isFinal)
    {
        _isCameraStopped = true;

        if (!isFinal) return;

        var levelCompleteScene = LevelCompleteScene.Instantiate();

        if (levelCompleteScene is not LevelComplete levelComplete)
            return;

        levelComplete.OnContinueToNextLevel += () =>
        {
            GetNode<LevelComplete>("LevelComplete").QueueFree();

            LoadNextLevel();
        };

        AddChild(levelComplete);

        EnemyManager.GameOver();
    }
}