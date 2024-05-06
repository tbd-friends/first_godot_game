using CanIDoThis.scripts.Managers;
using Godot;

namespace CanIDoThis.scripts;

public partial class GameManager : Node
{
    [Export] private InputManager InputManager;
    [Export] private TileMap WorldMap;
    [Export] private Camera Camera;

    private bool _isCameraStopped;

    public override void _EnterTree()
    {
        Camera.OnCameraStopped += OnCameraStopped;
        
        base._EnterTree();
    }

    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!_isCameraStopped)
        {
            Camera.Position += Vector2.Up * Camera.MovementSpeed * (float)delta;
        }
    }
    
    public override void _ExitTree()
    {
        Camera.OnCameraStopped -= OnCameraStopped;

        base._ExitTree();
    }

    private void OnCameraStopped()
    {
        _isCameraStopped = true;
    }
}