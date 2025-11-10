using System;
using System.Collections.Generic;

namespace Gameplay.Modifiers
{
    public class MagnitudeCalculatorsRepository
    {
        readonly Dictionary<string, IMagnitudeCalculator> outcomeMagnitudeCalculators;
        readonly Dictionary<string, IMagnitudeCalculator> incomeMagnitudeCalculators;

        public MagnitudeCalculatorsRepository(Random random)
        {
            outcomeMagnitudeCalculators = new Dictionary<string, IMagnitudeCalculator>
            {
                { "BaseParameter", new BaseParameterMagnitudeCalculator() },
                { "BaseDamage", new BaseDamageOutcomeMagnitudeCalculator(random) }
            };

            incomeMagnitudeCalculators = new Dictionary<string, IMagnitudeCalculator>
            {
                { "BaseParameter", new BaseParameterMagnitudeCalculator() },
                { "BaseDamage", new BaseDamageIncomeMagnitudeCalculator() }
            };
        }

        public IMagnitudeCalculator GetOutcomeMagnitudeCalculator(string id)
        {
            if (!outcomeMagnitudeCalculators.ContainsKey(id))
            {
                throw new Exception(
                    $"<MagnitudeCalculatorsRepository::GetOutcomeMagnitudeCalculator>: Calculator with ID {id} not found."
                );
            }

            return outcomeMagnitudeCalculators[id];
        }

        public IMagnitudeCalculator GetIncomeMagnitudeCalculator(string id)
        {
            if (!incomeMagnitudeCalculators.ContainsKey(id))
            {
                throw new Exception(
                    $"<MagnitudeCalculatorsRepository::GetIncomeMagnitudeCalculator>: Calculator with ID {id} not found."
                );
            }

            return incomeMagnitudeCalculators[id];
        }
    }
}
