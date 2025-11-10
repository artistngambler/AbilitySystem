using Attributes;
using Math;
using System;
using Attribute = Attributes.Attribute;

namespace Gameplay.Modifiers
{
    public class BaseDamageOutcomeMagnitudeCalculator : IMagnitudeCalculator
    {
        const int ExpectedParametersCount = 1;

        readonly Random random;

        float baseValue;
        AttributeSet attributes;

        public BaseDamageOutcomeMagnitudeCalculator(Random random)
        {
            this.random = random;
        }

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
                    $"<BaseDamageOutcomeMagnitudeCalculator::SetParameters>: Expected parameters count {ExpectedParametersCount}, got {parameters?.Length ?? 0}"
                );
            }

            baseValue = parameters[0];
        }

        public float Calculate()
        {
            if (attributes == null)
            {
                throw new Exception("<BaseDamageOutcomeMagnitudeCalculator::Calculate>: Attributes must be initialized.");
            }

            float magnitude = 0f;

            magnitude += baseValue;

            if (attributes.Has(Attribute.Attack))
            {
                magnitude += attributes.Get(Attribute.Attack).Value;
            }

            if (attributes.Has(Attribute.CriticalHitRate))
            {
                float rate = attributes.Get(Attribute.CriticalHitRate).Value;
                bool isCriticalHit = ProbabilityChecker.Check(rate, random);

                if (isCriticalHit)
                {
                    magnitude *= attributes.Get(Attribute.CriticalHit).Value;
                }
            }

            Reset();

            return magnitude;
        }

        void Reset()
        {
            baseValue = 0f;
            attributes = null;
        }
    }
}
