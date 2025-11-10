using Gameplay.Common;
using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class AbilityEffectCancellationSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter periodicFilter;
        EcsFilter nonPeriodicFilter;
        EcsFilter cancelledFilter;
        EcsPool<TargetComponent> targetPool;
        EcsPool<AbilityEffectModifierComponent> abilityEffectModifierPool;
        EcsPool<MagnitudeComponent> magnitudePool;
        EcsPool<CancelledFlagComponent> cancelledFlagPool;

        EffectFactory effectFactory;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            periodicFilter = world.Filter<AbilityEffectComponent>().Inc<PeriodComponent>().Inc<CanBeCancelledFlagComponent>()
               .End();

            nonPeriodicFilter = world.Filter<AbilityEffectComponent>().Inc<NonPeriodComponent>()
               .Inc<CanBeCancelledFlagComponent>().End();

            cancelledFilter = world.Filter<AbilityEffectComponent>().Inc<CancelledFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in periodicFilter)
            {
                CancelPeriodicEffect(entity);
            }

            foreach (int entity in nonPeriodicFilter)
            {
                CancelNonPeriodicEffect(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in cancelledFilter)
            {
                world.DelEntity(entity);
            }
        }

        void CancelPeriodicEffect(int entity)
        {
            cancelledFlagPool.Add(entity);
        }

        void CancelNonPeriodicEffect(int entity)
        {
            TargetComponent targetComponent = targetPool.Get(entity);
            AbilityEffectModifierComponent modifierComponent = abilityEffectModifierPool.Get(entity);
            MagnitudeComponent magnitudeComponent = magnitudePool.Get(entity);

            var effectModifier = new EffectModifier
            {
                Attribute = modifierComponent.Attribute,
                Operation = modifierComponent.Operation,
                Magnitude = -magnitudeComponent.Value,
                MagnitudeCalculationClass = modifierComponent.MagnitudeCalculationClass
            };

            effectFactory.Create(effectModifier, targetComponent.Value);

            cancelledFlagPool.Add(entity);
        }

        void InitPools()
        {
            targetPool = world.GetPool<TargetComponent>();
            abilityEffectModifierPool = world.GetPool<AbilityEffectModifierComponent>();
            magnitudePool = world.GetPool<MagnitudeComponent>();
            cancelledFlagPool = world.GetPool<CancelledFlagComponent>();
        }
    }
}
