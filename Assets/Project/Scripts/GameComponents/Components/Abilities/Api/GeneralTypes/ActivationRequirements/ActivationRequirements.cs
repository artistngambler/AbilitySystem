using Attributes;

namespace GameComponents.Abilities
{
    public class ActivationRequirements
    {
        public readonly Attribute Attribute;
        public readonly CompareOperation Operation;
        public readonly float Value;
        public readonly CompareValueType ValueType;

        public ActivationRequirements(Attribute attribute, CompareOperation operation, float value, CompareValueType valueType)
        {
            Attribute = attribute;
            Operation = operation;
            Value = value;
            ValueType = valueType;
        }
    }
}
