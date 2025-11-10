namespace GameComponents.Abilities
{
    public class Effect
    {
        public readonly DurationPolicy DurationPolicy;
        public readonly Duration Duration;
        public readonly Period Period;
        public readonly Modifier Modifier;
        public readonly float Rate;

        public Effect(DurationPolicy durationPolicy, Duration duration, Period period, Modifier modifier, float rate)
        {
            DurationPolicy = durationPolicy;
            Duration = duration;
            Period = period;
            Modifier = modifier;
            Rate = rate;
        }
    }
}
