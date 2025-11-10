namespace Math
{
    public static class FloatExtensions
    {
        public static bool IsZero(this float value)
        {
            return value < float.Epsilon && value > -float.Epsilon;
        }
    }
}
