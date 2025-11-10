using Gameplay.Common;
using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class EffectBuilder
    {
        readonly EntityBuilder entityBuilder;

        public EffectBuilder(EcsWorld world, EffectModifier modifier, EcsPackedEntity targetEntity)
        {
            entityBuilder = new EntityBuilder(world);

            var effectComponent = new EffectComponent();
            var targetComponent = new TargetComponent { Value = targetEntity };

            entityBuilder.With(effectComponent).With(targetComponent);

            AddModifier(modifier);
        }

        public EcsPackedEntity Build()
        {
            return entityBuilder.Build();
        }

        void AddModifier(EffectModifier modifier)
        {
            if (modifier == null)
            {
                return;
            }

            var component = new EffectModifierComponent
            {
                Attribute = modifier.Attribute,
                Operation = modifier.Operation,
                Magnitude = modifier.Magnitude,
                MagnitudeCalculationClass = modifier.MagnitudeCalculationClass
            };

            entityBuilder.With(component);
        }
    }
}
