namespace GameComponents.Abilities
{
    public class Effect
    {
        public readonly DurationPolicy DurationPolicy;
        public readonly Duration Duration;
        public readonly Period Period;
        public readonly Modifier Modifier;

        public Effect(DurationPolicy durationPolicy, Duration duration, Period period, Modifier modifier)
        {
            DurationPolicy = durationPolicy;
            Duration = duration;
            Period = period;
            Modifier = modifier;
        }
    }
}
