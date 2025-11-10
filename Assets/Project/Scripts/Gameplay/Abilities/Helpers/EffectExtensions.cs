using GameComponents.Abilities;

namespace Gameplay.Abilities
{
    public static class EffectExtensions
    {
        public static AbilityEffect ToAbilityEffect(this Effect effect)
        {
            var abilityEffect = new AbilityEffect
            {
                DurationPolicy = effect.DurationPolicy,
                Rate = effect.Rate
            };

            if (effect.Duration != null)
            {
                abilityEffect.Duration = new Duration { Value = effect.Duration.Value };
            }

            if (effect.Period != null)
            {
                abilityEffect.Period = new Period { Value = effect.Period.Value };
            }

            if (effect.Modifier != null)
            {
                abilityEffect.Modifier = new AbilityEffectModifier
                {
                    Attribute = effect.Modifier.Attribute,
                    Operation = effect.Modifier.Operation,
                    Parameters = new float[effect.Modifier.Parameters.Length],
                    MagnitudeCalculationClass = effect.Modifier.MagnitudeCalculationClass
                };

                for (int i = 0; i < effect.Modifier.Parameters.Length; i++)
                {
                    abilityEffect.Modifier.Parameters[i] = effect.Modifier.Parameters[i];
                }
            }

            return abilityEffect;
        }
    }
}
