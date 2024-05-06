using System;
using System.Diagnostics;
using Godot;

public partial class Camera : Camera2D
{
    [Export] public float MovementSpeed { get; private set; } = 100.0f;

    public event Action OnCameraStopped;

    private Area2D _area2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _area2D = GetNode<Area2D>("Stopper");
        _area2D.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is not CameraStop { IsFinal: true })
        {
            return;
        }
        
        OnCameraStopped?.Invoke();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}