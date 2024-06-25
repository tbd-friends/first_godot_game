using System.Collections.Generic;
using System.Linq;
using CanIDoThis.scripts.Contracts;
using Godot;

namespace CanIDoThis.scripts;

public partial class WaveTrigger : CameraStop
{
    private IEnumerable<IWaveTriggered> _wave;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;

        _wave = from c in GetChildren()
            where c is IWaveTriggered
            select c as IWaveTriggered;
    }

    private void OnAreaEntered(Area2D area)
    {
        foreach (var element in _wave)
        {
            element.NotifyWaveTriggered();
        }
    }
}