namespace JustMoby_TestWork
{
    public sealed class TakeHealSignal
    {
        public HealthParameter Health { get; }

        public TakeHealSignal(HealthParameter health)
        {
            Health = health;
        }
    }
}
