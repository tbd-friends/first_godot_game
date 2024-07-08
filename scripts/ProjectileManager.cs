using System.Collections.Generic;
using CanIDoThis.scripts.Components;
using Godot;

namespace CanIDoThis.scripts;

public partial class ProjectileManager : Node2D
{
    private List<Projectile> _projectiles = new();

    public Projectile SpawnProjectile(PackedScene projectileScene, Node2D origin)
    {
        Projectile projectile = projectileScene.Instantiate<Projectile>();

        AddChild(projectile);

        projectile.SetOrigin(origin);

        _projectiles.Add(projectile);

        projectile.TreeExiting += () => { _projectiles.Remove(projectile); };

        return projectile;
    }
    
    public void ClearProjectiles()
    {
        foreach (Projectile projectile in _projectiles)
        {
            projectile.QueueFree();
        }

        _projectiles.Clear();
    }
}