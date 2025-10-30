using Attributes;

namespace GameComponents.Abilities
{
    public class Modifier
    {
        public readonly Attribute Attribute;
        public readonly ModifyOperation Operation;
        public readonly float[] Parameters;
        public readonly string MagnitudeCalculationClass;

        public Modifier(Attribute attribute, ModifyOperation operation, float[] parameters, string magnitudeCalculationClass)
        {
            Attribute = attribute;
            Operation = operation;
            Parameters = parameters;
            MagnitudeCalculationClass = magnitudeCalculationClass;
        }
    }
}
