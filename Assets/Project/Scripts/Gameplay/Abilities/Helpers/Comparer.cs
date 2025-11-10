using GameComponents.Abilities;
using Math;
using System;

namespace Gameplay.Abilities
{
    public static class Comparer
    {
        public static bool Compare(CompareOperation operation, float value, float targetValue)
        {
            return operation switch
            {
                CompareOperation.Greater => value > targetValue,
                CompareOperation.Less => value < targetValue,
                CompareOperation.Equal => (value - targetValue).IsZero(),
                CompareOperation.GreaterOrEqual => value >= targetValue,
                CompareOperation.LessOrEqual => value <= targetValue,
                CompareOperation.NotEqual => !(value - targetValue).IsZero(),
                _ => throw new Exception($"<Comparer::Compare>: Unknown operation {operation}")
            };
        }
    }
}
