using Attributes;
using GameComponents.Abilities;

namespace Gameplay.Abilities
{
    public struct ActivationRequirementsComponent
    {
        public Attribute Attribute;
        public CompareOperation Operation;
        public float Value;
        public CompareValueType ValueType;
    }
}
