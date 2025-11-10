using GameComponents.Abilities;

namespace Gameplay.Abilities
{
    public class AbilityEffect
    {
        public DurationPolicy DurationPolicy;
        public Duration Duration;
        public Period Period;
        public AbilityEffectModifier Modifier;
        public float Rate;
    }
}
