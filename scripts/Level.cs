using CanIDoThis.scripts.Managers;
using Godot;

namespace CanIDoThis.scripts;

public partial class Level : Node
{
    [Export] public TileMap Map { get; set; }
    public EnemyManager EnemyManager { get; set; }
}