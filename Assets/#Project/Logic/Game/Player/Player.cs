using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class Player : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public Transform GunShootPoint { get; private set; }

        private HealthParameter _healthParameter;

        [Inject]
        private void Construct(HealthParameter healthParameter)
        {
            _healthParameter = healthParameter;
        }

        public void TakeDamage(int damage, Vector3 hitPoint)
        {
            _healthParameter?.TakeDamage(damage, hitPoint);
        }

        private void Reset()
        {
            AutoAssign();
        }

        private void OnValidate()
        {
            AutoAssign();
        }

        private void AutoAssign()
        {
            Rigidbody ??= GetComponent<Rigidbody>();
            Animator ??= GetComponentInChildren<Animator>();
        }
    }
}
