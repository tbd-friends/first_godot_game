using Godot;

namespace CanIDoThis.scripts.Enemies;

public abstract partial class PlayerTrackingEnemy : EnemyBase
{
    [Export, ExportCategory("Tracking")] public Radar Radar { get; set; }
}