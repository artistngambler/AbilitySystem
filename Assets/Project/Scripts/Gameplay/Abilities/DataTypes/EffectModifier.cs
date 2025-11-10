using Attributes;
using GameComponents.Abilities;

namespace Gameplay.Abilities
{
    public class EffectModifier
    {
        public Attribute Attribute;
        public ModifyOperation Operation;
        public float Magnitude;
        public string MagnitudeCalculationClass;
    }
}
