namespace Enemy
{
    public interface IAborigineTeamData : IEnemyTeamData
    {
        int MeleeCount { get; }
        int RangedCount { get; }
    }
}