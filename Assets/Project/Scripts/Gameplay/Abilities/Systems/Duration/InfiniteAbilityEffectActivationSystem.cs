using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class InfiniteAbilityEffectActivationSystem
        : IEcsInitSystem,
          IEcsRunSystem
    {
        EcsWorld world;
        EcsFilter periodFilter;
        EcsFilter nonPeriodFilter;
        EcsPool<ApplicationRequestComponent> applicationRequestPool;
        EcsPool<ResetPeriodRequestComponent> resetPeriodRequestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            periodFilter = world.Filter<AbilityEffectComponent>().Inc<InfiniteComponent>().Inc<PeriodComponent>()
               .Inc<CanBeActivatedFlagComponent>().Exc<ResetPeriodRequestComponent>().End();

            nonPeriodFilter = world.Filter<AbilityEffectComponent>().Inc<InfiniteComponent>().Inc<NonPeriodComponent>()
               .Inc<CanBeActivatedFlagComponent>().Exc<ApplicationRequestComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in periodFilter)
            {
                resetPeriodRequestPool.Add(entity);
            }

            foreach (int entity in nonPeriodFilter)
            {
                applicationRequestPool.Add(entity);
            }
        }

        void InitPools()
        {
            applicationRequestPool = world.GetPool<ApplicationRequestComponent>();
            resetPeriodRequestPool = world.GetPool<ResetPeriodRequestComponent>();
        }
    }
}
