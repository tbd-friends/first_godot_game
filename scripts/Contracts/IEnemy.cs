namespace CanIDoThis.scripts.Contracts;

public interface IEnemy : ICareAboutGameOver, IScoreable
{
    Radar Radar { get; set; }
}