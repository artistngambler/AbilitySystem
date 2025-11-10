using GameComponents.Abilities;
using Gameplay.Common;
using Leopotam.EcsLite;
using System;
using System.Linq;

namespace Gameplay.Abilities
{
    public class AbilityEffectBuilder
    {
        readonly EntityBuilder entityBuilder;

        public AbilityEffectBuilder(EcsWorld world, AbilityEffect effect, EcsPackedEntity targetEntity)
        {
            entityBuilder = new EntityBuilder(world);

            var effectComponent = new AbilityEffectComponent { Rate = effect.Rate };
            var targetComponent = new TargetComponent { Value = targetEntity };

            entityBuilder.With(effectComponent).With(targetComponent);

            AddDuration(effect);
            AddModifier(effect.Modifier);

            if (effect.DurationPolicy == DurationPolicy.HasDuration
             || effect.DurationPolicy == DurationPolicy.Infinite)
            {
                AddPeriod(effect.Period);
            }
        }

        public EcsPackedEntity Build()
        {
            return entityBuilder.Build();
        }

        void AddDuration(AbilityEffect effect)
        {
            switch (effect.DurationPolicy)
            {
                case DurationPolicy.Instant:
                    var instantComponent = new InstantComponent();
                    entityBuilder.With(instantComponent);

                    break;

                case DurationPolicy.HasDuration:
                    var hasDurationComponent = new HasDurationComponent { Value = effect.Duration.Value };
                    entityBuilder.With(hasDurationComponent);

                    break;

                case DurationPolicy.Infinite:
                    var infiniteComponent = new InfiniteComponent();
                    entityBuilder.With(infiniteComponent);

                    break;

                default:
                    throw new Exception($"<AbilityEffectBuilder::AddDuration>: Unknown duration policy: {effect.DurationPolicy}");
            }
        }

        void AddPeriod(Period period)
        {
            if (period == null)
            {
                entityBuilder.With(new NonPeriodComponent());

                return;
            }

            var component = new PeriodComponent { Value = period.Value };
            entityBuilder.With(component);
        }

        void AddModifier(AbilityEffectModifier modifier)
        {
            if (modifier == null)
            {
                return;
            }

            var component = new AbilityEffectModifierComponent
            {
                Attribute = modifier.Attribute,
                Operation = modifier.Operation,
                Parameters = modifier.Parameters.ToArray(),
                MagnitudeCalculationClass = modifier.MagnitudeCalculationClass
            };

            entityBuilder.With(component);
        }
    }
}
