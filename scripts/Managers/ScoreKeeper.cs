using System;
using System.Collections.Generic;
using System.Linq;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class ScoreKeeper : Node
{
    public event Action<int> ScoreChanged;
    public int Score { get; private set; }

    public void OnLevelChanged()
    {
        IEnumerable<IScoreable> enemiesToSubscribeTo = GetTree()
            .GetNodesInGroup("Enemies")
            .OfType<IScoreable>();

        foreach (IScoreable enemy in enemiesToSubscribeTo)
        {
            enemy.CollisionOccured += OnEnemyCollision;
        }
    }

    private void OnEnemyCollision(IScoreable enemy)
    {
        Score += enemy.ScoreValue;

        enemy.CollisionOccured -= OnEnemyCollision;

        ScoreChanged?.Invoke(Score);
    }

    public void Reset()
    {
        Score = 0;
        
        ScoreChanged?.Invoke(Score);
    }
}