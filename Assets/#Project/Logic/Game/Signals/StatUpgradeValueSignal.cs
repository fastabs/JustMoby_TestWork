namespace JustMoby_TestWork
{
    public sealed class StatUpgradeValueSignal
    {
        public Stat Stat { get; }
        public int Value { get; }

        public StatUpgradeValueSignal(Stat stat, int value)
        {
            Stat = stat;
            Value = value;
        }
    }
}
