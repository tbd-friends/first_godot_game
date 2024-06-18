using Godot;
using System;

public partial class LevelComplete : Node
{
    public event Action OnContinueToNextLevel;

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("shoot"))
        {
            OnContinueToNextLevel?.Invoke();
        }
    }
}