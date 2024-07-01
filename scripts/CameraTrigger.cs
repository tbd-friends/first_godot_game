using Godot;

namespace CanIDoThis.scripts;

public partial class CameraTrigger : Area2D
{
    [Export] public CollisionShape2D Collider { get; set; }
}