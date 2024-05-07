using System;

namespace CanIDoThis.scripts.Contracts;

public interface IScoreable
{
    public event Action<IScoreable> CollisionOccured;
    int Score { get; set; }
}