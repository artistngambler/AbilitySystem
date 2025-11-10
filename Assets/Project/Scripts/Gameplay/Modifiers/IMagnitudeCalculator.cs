using Attributes;

namespace Gameplay.Modifiers
{
    public interface IMagnitudeCalculator
    {
        void SetAttributes(AttributeSet attributes);
        void SetParameters(float[] parameters);
        float Calculate();
    }
}
