using System;
using System.Collections.Generic;
using System.Linq;
using CanIDoThis.scripts.Contracts;
using CanIDoThis.scripts.Enemies;
using Godot;

namespace CanIDoThis.scripts.Managers;

public partial class EnemyManager : Node
{
    public event Action OnGameOver;
    [Export] public Radar Radar { get; set; }

    private List<IEnemy> _enemies = [];

    public void OnLevelChanged()
    {
        _enemies?.Clear();

        _enemies = GetTree()
            .GetNodesInGroup("Enemies")
            .OfType<IEnemy>()
            .ToList();

        foreach (var enemy in _enemies)
        {
            if (enemy is PlayerTrackingEnemy tracking)
            {
                tracking.Radar = Radar;
            }

            OnGameOver += enemy.NotifyOnGameOver;

            enemy.CollisionOccured += NotifyCollision;
        }
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();

        _enemies.Clear();
    }

    private void NotifyCollision(IScoreable scored)
    {
        if (scored is not IEnemy enemy)
        {
            return;
        }

        _enemies.Remove(enemy);

        enemy.CollisionOccured -= NotifyCollision;

        OnGameOver -= enemy.NotifyOnGameOver;
    }
}