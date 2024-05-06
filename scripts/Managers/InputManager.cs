using Godot;

namespace CanIDoThis.scripts.Managers;

public partial class InputManager : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("scroll_left"))
        {
            GD.Print("Scrolling Left");
        }

        if (Input.IsActionPressed("scroll_right"))
        {
            GD.Print("Scrolling Right");
        }
    }
}