namespace JustMoby_TestWork
{
    public sealed class StatUpgradeCreatedSignal
    {
        public Stat Stat { get; }

        public StatUpgradeCreatedSignal(Stat stat)
        {
            Stat = stat;
        }
    }
}
