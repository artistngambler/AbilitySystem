using Leopotam.EcsLite;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class DurationTimerSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsFilter finishedFilter;
        EcsPool<DurationTimerComponent> timerPool;
        EcsPool<DurationFinishedFlagComponent> finishedFlagPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<AbilityEffectComponent>().Inc<DurationTimerComponent>().End();
            finishedFilter = world.Filter<AbilityEffectComponent>().Inc<DurationFinishedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                ref DurationTimerComponent timerComponent = ref timerPool.Get(entity);
                timerComponent.TimeLeft -= Time.deltaTime;

                if (timerComponent.TimeLeft > 0)
                {
                    continue;
                }

                finishedFlagPool.Add(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in finishedFilter)
            {
                timerPool.Del(entity);
                finishedFlagPool.Del(entity);
            }
        }

        void InitPools()
        {
            timerPool = world.GetPool<DurationTimerComponent>();
            finishedFlagPool = world.GetPool<DurationFinishedFlagComponent>();
        }
    }
}
