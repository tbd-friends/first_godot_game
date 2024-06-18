using System;
using Godot;

public partial class Camera : Camera2D
{
    [Export] public float MovementSpeed { get; private set; } = 100.0f;

    public event Action<bool> OnCameraStopped;

    private Area2D _cameraStop;

    public override void _Ready()
    {
        _cameraStop = GetNode<Area2D>("Stopper");
        _cameraStop.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is not CameraStop cameraStop)
        {
            return;
        }

        OnCameraStopped?.Invoke(cameraStop.IsFinal);
    }
}