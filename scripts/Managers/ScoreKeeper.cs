using System.Collections.Generic;
using System.Linq;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class ScoreKeeper : Node
{
    public int Score { get; private set; }

    public override void _Ready()
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
        Score += enemy.Score;
    }
}