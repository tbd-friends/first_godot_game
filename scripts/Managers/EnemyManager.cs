using System;
using System.Collections.Generic;
using System.Linq;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class EnemyManager : Node
{
    public event Action OnGameOver;

    private List<IEnemy> _enemies = new();

    public override void _Ready()
    {
        _enemies = GetTree()
            .GetNodesInGroup("Enemies")
            .OfType<IEnemy>()
            .ToList();

        foreach (var enemy in _enemies)
        {
            OnGameOver += enemy.NotifyOnGameOver;

            enemy.CollisionOccured += NotifyCollision;
        }
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
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