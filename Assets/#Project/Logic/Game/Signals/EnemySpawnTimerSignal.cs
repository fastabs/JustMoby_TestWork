namespace JustMoby_TestWork
{
    public sealed class EnemySpawnTimerSignal
    {
        public float SecondsRemaining { get; }

        public EnemySpawnTimerSignal(float secondsRemaining)
        {
            SecondsRemaining = secondsRemaining;
        }
    }
}
