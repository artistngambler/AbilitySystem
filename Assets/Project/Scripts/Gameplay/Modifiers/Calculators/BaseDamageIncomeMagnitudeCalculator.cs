using Attributes;
using System;
using Attribute = Attributes.Attribute;

namespace Gameplay.Modifiers
{
    public class BaseDamageIncomeMagnitudeCalculator : IMagnitudeCalculator
    {
        const int ExpectedParametersCount = 1;

        float baseValue;
        AttributeSet attributes;

        public void SetAttributes(AttributeSet attributes)
        {
            this.attributes = attributes;
        }

        public void SetParameters(float[] parameters)
        {
            if (parameters == null
             || parameters.Length != ExpectedParametersCount)
            {
                throw new Exception(
                    $"<BaseDamageIncomeMagnitudeCalculator::SetParameters>: Expected parameters count {ExpectedParametersCount}, got {parameters?.Length ?? 0}"
                );
            }

            baseValue = parameters[0];
        }

        public float Calculate()
        {
            if (attributes == null)
            {
                throw new Exception("<BaseDamageIncomeMagnitudeCalculator::Calculate>: Attributes must be initialized.");
            }

            float magnitude = 0f;

            magnitude += baseValue;

            if (attributes.Has(Attribute.Defense))
            {
                magnitude -= attributes.Get(Attribute.Defense).Value;
            }

            if (magnitude < 0f)
            {
                magnitude = 0f;
            }

            Reset();

            return -magnitude;
        }

        void Reset()
        {
            baseValue = 0f;
            attributes = null;
        }
    }
}
