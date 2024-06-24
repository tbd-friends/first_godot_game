using System;
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
    private event Action LevelChanged;

    private bool _isCameraStopped;
    private int _currentLevel = 0;

    public override void _EnterTree()
    {
        Camera.OnCameraStopped += OnCameraStopped;

        base._EnterTree();
    }

    public override void _Ready()
    {
        Player.DeathOccurred += OnPlayerDeath;

        LevelChanged += ScoreKeeper.OnLevelChanged;
        LevelChanged += EnemyManager.OnLevelChanged;

        LoadNextLevel();
    }

    private bool ShouldLoadNextLevel() => _currentLevel < Levels.Count;

    private void LoadNextLevel()
    {
        if (Levels[_currentLevel].Instantiate() is not Level level)
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

        LevelChanged?.Invoke();

        _currentLevel++;

        _isCameraStopped = false;
    }

    private void OnPlayerDeath(Player player)
    {
        _isCameraStopped = true;

        AddGameOverScene();

        EnemyManager.GameOver();
    }

    private void AddGameOverScene()
    {
        var gameOverScene = GameOverScene.Instantiate();

        if (gameOverScene is GameOver gameOver)
        {
            gameOver.OnRestartGame += () =>
            {
                _currentLevel = 0;

                GetNode<GameOver>("GameOver").QueueFree();

                LoadNextLevel();
            };

            gameOver.OnEndOfGame += () =>
            {
                Player.QueueFree();
                EnemyManager.QueueFree();
                ProjectileManager.QueueFree();
                ScoreKeeper.QueueFree();
            };
        }

        AddChild(gameOverScene);
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

        if (!isFinal)
        {
            return;
        }

        if (ShouldLoadNextLevel())
        {
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
        else
        {
            AddGameOverScene();
        }
    }
}