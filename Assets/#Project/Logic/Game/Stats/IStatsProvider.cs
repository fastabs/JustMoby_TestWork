namespace JustMoby_TestWork
{
    public interface IStatsProvider
    {
        MoveSpeedStat MoveSpeed { get; }
        MaxHealthStat MaxHealth { get; }
        DamageStat Damage { get; }
    }
}
