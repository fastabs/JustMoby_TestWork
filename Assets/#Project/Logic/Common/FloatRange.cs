using System;
using Random = UnityEngine.Random;

namespace JustMoby_TestWork
{
    [Serializable]
    public struct FloatRange
    {
        public float Min;
        public float Max;

        public float GetRandom()
        {
            return Random.Range(Min, Max);
        }
    }
}
