using UnityEngine;

namespace JustMoby_TestWork
{
    public sealed class TakeDamageSignal
    {
        public Vector3 HitPoint { get; }
        public HealthParameter Health { get; }

        public TakeDamageSignal(Vector3 hitPoint, HealthParameter health)
        {
            HitPoint = hitPoint;
            Health = health;
        }
    }
}
