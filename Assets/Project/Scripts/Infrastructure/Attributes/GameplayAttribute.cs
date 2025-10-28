namespace Attributes
{
    public class GameplayAttribute
    {
        public float Value => GetValue();

        readonly float baseValue;

        float additiveValue;
        float multiplicativeValue;
        float minValue;
        float maxValue;

        public GameplayAttribute()
        {
            baseValue = 0f;
            additiveValue = 0f;
            multiplicativeValue = 0f;
            minValue = float.MinValue;
            maxValue = float.MaxValue;
        }

        public GameplayAttribute(float baseValue)
        {
            this.baseValue = baseValue;
            additiveValue = 0f;
            multiplicativeValue = 0f;
            minValue = float.MinValue;
            maxValue = float.MaxValue;
        }

        public GameplayAttribute(float baseValue, float additiveValue, float multiplicativeValue)
        {
            this.baseValue = baseValue;
            this.additiveValue = additiveValue;
            this.multiplicativeValue = multiplicativeValue;
            minValue = float.MinValue;
            maxValue = float.MaxValue;
        }

        public void AddAdditive(float value)
        {
            additiveValue += value;
        }

        public void AddMultiplicative(float value)
        {
            multiplicativeValue += value;
        }

        public void SetMinValue(float value)
        {
            minValue = value;
        }

        public void SetMaxValue(float value)
        {
            maxValue = value;
        }

        float GetValue()
        {
            float result = (baseValue + additiveValue) * (1f + multiplicativeValue / 100f);

            if (result > maxValue)
            {
                result = maxValue;
            }
            else if (result < minValue)
            {
                result = minValue;
            }

            return result;
        }
    }
}
