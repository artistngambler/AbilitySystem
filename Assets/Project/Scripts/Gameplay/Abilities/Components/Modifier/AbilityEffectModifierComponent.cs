using Attributes;
using GameComponents.Abilities;

namespace Gameplay.Abilities
{
    public struct AbilityEffectModifierComponent
    {
        public Attribute Attribute;
        public ModifyOperation Operation;
        public float[] Parameters;
        public string MagnitudeCalculationClass;
    }
}
