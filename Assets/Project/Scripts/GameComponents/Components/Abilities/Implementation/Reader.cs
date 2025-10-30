using GameComponents.Abilities.ExternalTypes;
using System.Collections.Generic;

namespace GameComponents.Abilities
{
    public class Reader : IReader
    {
        readonly IRepository repository;

        public Reader(IRepository repository)
        {
            this.repository = repository;
        }

        public Ability Get(string id)
        {
            InternalTypes.Ability abilityInternal = repository.Get(id);

            var activationRequirements = new ActivationRequirements(
                abilityInternal.ActivationRequirements.Attribute,
                abilityInternal.ActivationRequirements.Operation,
                abilityInternal.ActivationRequirements.Value,
                abilityInternal.ActivationRequirements.ValueType
            );

            var cast = new Cast(abilityInternal.Cast.Value);
            var cooldown = new Cooldown(abilityInternal.Cooldown.Value);

            var effects = new List<Effect>();

            foreach (Effect effectInternal in abilityInternal.Effects)
            {
                var duration = new Duration(effectInternal.Duration.Value);
                var period = new Period(effectInternal.Period.Value);

                int parametersCount = effectInternal.Modifier.Parameters.Length;
                float[] parameters = new float[parametersCount];

                for (int i = 0; i < parametersCount - 1; i++)
                {
                    parameters[i] = effectInternal.Modifier.Parameters[i];
                }

                var modifier = new Modifier(
                    effectInternal.Modifier.Attribute,
                    effectInternal.Modifier.Operation,
                    parameters,
                    effectInternal.Modifier.MagnitudeCalculationClass
                );

                var effect = new Effect(effectInternal.DurationPolicy, duration, period, modifier);
                effects.Add(effect);
            }

            return new Ability(
                abilityInternal.Id,
                abilityInternal.Type,
                activationRequirements,
                cast,
                cooldown,
                effects.ToArray()
            );
        }
    }
}
