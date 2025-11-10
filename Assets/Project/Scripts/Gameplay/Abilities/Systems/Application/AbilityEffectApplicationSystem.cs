using Gameplay.Common;
using Gameplay.Modifiers;
using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class AbilityEffectApplicationSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        readonly MagnitudeCalculatorsRepository magnitudeCalculatorsRepository;

        EcsWorld world;
        EcsFilter filter;
        EcsFilter appliedFilter;
        EcsPool<TargetComponent> targetPool;
        EcsPool<SourceComponent> sourcePool;
        EcsPool<AttributeSetComponent> attributeSetPool;
        EcsPool<AbilityEffectModifierComponent> abilityEffectModifierPool;
        EcsPool<NonPeriodComponent> nonPeriodPool;
        EcsPool<MagnitudeComponent> magnitudePool;
        EcsPool<AppliedFlagComponent> appliedFlagPool;

        EffectFactory effectFactory;

        public AbilityEffectApplicationSystem(MagnitudeCalculatorsRepository magnitudeCalculatorsRepository)
        {
            this.magnitudeCalculatorsRepository = magnitudeCalculatorsRepository;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<AbilityEffectComponent>().Inc<CanBeAppliedFlagComponent>().End();
            appliedFilter = world.Filter<AbilityEffectComponent>().Inc<AppliedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                TargetComponent targetComponent = targetPool.Get(entity);
                SourceComponent sourceComponent = sourcePool.Get(entity);
                AttributeSetComponent attributeSetComponent = attributeSetPool.Get(sourceComponent.Value.Id);
                AbilityEffectModifierComponent modifierComponent = abilityEffectModifierPool.Get(entity);

                IMagnitudeCalculator magnitudeCalculator =
                    magnitudeCalculatorsRepository.GetOutcomeMagnitudeCalculator(modifierComponent.MagnitudeCalculationClass);

                magnitudeCalculator.SetAttributes(attributeSetComponent.Value);
                magnitudeCalculator.SetParameters(modifierComponent.Parameters);

                float magnitude = magnitudeCalculator.Calculate();

                var effectModifier = new EffectModifier
                {
                    Attribute = modifierComponent.Attribute,
                    Operation = modifierComponent.Operation,
                    Magnitude = magnitude,
                    MagnitudeCalculationClass = modifierComponent.MagnitudeCalculationClass
                };

                if (nonPeriodPool.Has(entity))
                {
                    ref MagnitudeComponent magnitudeComponent = ref magnitudePool.Add(entity);
                    magnitudeComponent.Value = magnitude;
                }

                effectFactory.Create(effectModifier, targetComponent.Value);

                appliedFlagPool.Add(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in appliedFilter)
            {
                appliedFlagPool.Del(entity);
            }
        }

        void InitPools()
        {
            targetPool = world.GetPool<TargetComponent>();
            sourcePool = world.GetPool<SourceComponent>();
            attributeSetPool = world.GetPool<AttributeSetComponent>();
            abilityEffectModifierPool = world.GetPool<AbilityEffectModifierComponent>();
            nonPeriodPool = world.GetPool<NonPeriodComponent>();
            magnitudePool = world.GetPool<MagnitudeComponent>();
            appliedFlagPool = world.GetPool<AppliedFlagComponent>();
        }
    }
}
