using UnityEngine;

namespace JustMoby_TestWork
{
    public sealed class Player : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public Transform GunShootPoint { get; private set; }
    }
}