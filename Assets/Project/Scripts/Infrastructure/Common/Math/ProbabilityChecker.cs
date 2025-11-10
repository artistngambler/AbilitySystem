using System;

namespace Math
{
    public static class ProbabilityChecker
    {
        public static bool Check(float value, Random random)
        {
            return value >= random.NextDouble();
        }
    }
}
