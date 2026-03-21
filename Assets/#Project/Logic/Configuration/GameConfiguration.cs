using System;
using UnityEngine;

namespace JustMoby_TestWork
{
    [CreateAssetMenu(menuName = "Configs/GameConfiguration", fileName = nameof(GameConfiguration))]
    public sealed class GameConfiguration : ScriptableObject
    {
        [field: SerializeField] public PlayerConfig Player { get; private set; }

        [Serializable]
        public struct PlayerConfig
        {
            [field: SerializeField] public float MoveSpeed { get; private set; }
            [field: SerializeField] public float RotationSpeed { get; private set; }
        }
    }
}

