namespace JustMoby_TestWork
{
    public sealed class DeathSignal
    {
        public HealthParameter Health { get; }

        public DeathSignal(HealthParameter health)
        {
            Health = health;
        }
    }
}
