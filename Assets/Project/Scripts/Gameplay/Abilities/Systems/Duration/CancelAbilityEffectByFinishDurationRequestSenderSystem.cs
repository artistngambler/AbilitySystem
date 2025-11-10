using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class CancelAbilityEffectByFinishDurationRequestSenderSystem
        : IEcsInitSystem,
          IEcsRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsPool<CancellationRequestComponent> cancellationRequestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<AbilityEffectComponent>().Inc<DurationFinishedFlagComponent>()
               .Exc<CancellationRequestComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                cancellationRequestPool.Add(entity);
            }
        }

        void InitPools()
        {
            cancellationRequestPool = world.GetPool<CancellationRequestComponent>();
        }
    }
}
