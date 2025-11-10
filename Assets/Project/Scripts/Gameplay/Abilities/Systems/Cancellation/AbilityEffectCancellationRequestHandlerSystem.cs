using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class AbilityEffectCancellationRequestHandlerSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsFilter canBeCancelledFilter;
        EcsPool<CancellationRequestComponent> cancellationRequestPool;
        EcsPool<CanBeCancelledFlagComponent> canBeCancelledFlagPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<AbilityEffectComponent>().Inc<CancellationRequestComponent>().End();
            canBeCancelledFilter = world.Filter<AbilityEffectComponent>().Inc<CanBeCancelledFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                cancellationRequestPool.Del(entity);
                canBeCancelledFlagPool.Add(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in canBeCancelledFilter)
            {
                canBeCancelledFlagPool.Del(entity);
            }
        }

        void InitPools()
        {
            cancellationRequestPool = world.GetPool<CancellationRequestComponent>();
            canBeCancelledFlagPool = world.GetPool<CanBeCancelledFlagComponent>();
        }
    }
}
