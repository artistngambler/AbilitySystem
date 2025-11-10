using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class HasDurationAbilityEffectActivationSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter periodFilter;
        EcsFilter nonPeriodFilter;
        EcsFilter durationStartedFilter;
        EcsPool<ApplicationRequestComponent> applicationRequestPool;
        EcsPool<ResetPeriodRequestComponent> resetPeriodRequestPool;
        EcsPool<DurationStartedFlagComponent> durationStartedFlagPool;
        EcsPool<HasDurationComponent> hasDurationPool;
        EcsPool<DurationTimerComponent> durationTimerPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            periodFilter = world.Filter<AbilityEffectComponent>().Inc<HasDurationComponent>().Inc<PeriodComponent>()
               .Inc<CanBeActivatedFlagComponent>().Exc<ResetPeriodRequestComponent>().End();

            nonPeriodFilter = world.Filter<AbilityEffectComponent>().Inc<HasDurationComponent>().Inc<NonPeriodComponent>()
               .Inc<CanBeActivatedFlagComponent>().Exc<ApplicationRequestComponent>().End();

            durationStartedFilter = world.Filter<AbilityEffectComponent>().Inc<HasDurationComponent>()
               .Inc<DurationStartedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in periodFilter)
            {
                resetPeriodRequestPool.Add(entity);
                AddTimer(entity);
            }

            foreach (int entity in nonPeriodFilter)
            {
                applicationRequestPool.Add(entity);
                AddTimer(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in durationStartedFilter)
            {
                durationStartedFlagPool.Del(entity);
            }
        }

        void AddTimer(int entity)
        {
            HasDurationComponent hasDurationComponent = hasDurationPool.Get(entity);
            ref DurationTimerComponent durationTimerComponent = ref durationTimerPool.Add(entity);
            durationTimerComponent.TimeLeft = hasDurationComponent.Value;
        }

        void InitPools()
        {
            applicationRequestPool = world.GetPool<ApplicationRequestComponent>();
            resetPeriodRequestPool = world.GetPool<ResetPeriodRequestComponent>();
            durationStartedFlagPool = world.GetPool<DurationStartedFlagComponent>();
            hasDurationPool = world.GetPool<HasDurationComponent>();
            durationTimerPool = world.GetPool<DurationTimerComponent>();
        }
    }
}
