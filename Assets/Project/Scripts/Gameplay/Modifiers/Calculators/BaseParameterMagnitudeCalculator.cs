using Attributes;
using System;

namespace Gameplay.Modifiers
{
    public class BaseParameterMagnitudeCalculator : IMagnitudeCalculator
    {
        const int ExpectedParametersCount = 1;

        float baseValue;

        public void SetAttributes(AttributeSet attributes) { }

        public void SetParameters(float[] parameters)
        {
            if (parameters == null
             || parameters.Length != ExpectedParametersCount)
            {
                throw new Exception(
                    $"<BaseValueMagnitudeCalculator::SetParameters>: Expected parameters count {ExpectedParametersCount}, got {parameters?.Length ?? 0}"
                );
            }

            baseValue = parameters[0];
        }

        public float Calculate()
        {
            float magnitude = 0f;

            magnitude += baseValue;

            Reset();

            return magnitude;
        }

        void Reset()
        {
            baseValue = 0f;
        }
    }
}
