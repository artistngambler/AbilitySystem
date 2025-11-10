using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class AbilityEffectApplicationRequestHandlerSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsFilter canBeAppliedFilter;
        EcsPool<ApplicationRequestComponent> applicationRequestPool;
        EcsPool<CanBeAppliedFlagComponent> canBeAppliedFlagPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<AbilityEffectComponent>().Inc<ApplicationRequestComponent>().End();
            canBeAppliedFilter = world.Filter<AbilityEffectComponent>().Inc<CanBeAppliedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                applicationRequestPool.Del(entity);
                canBeAppliedFlagPool.Add(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in canBeAppliedFilter)
            {
                canBeAppliedFlagPool.Del(entity);
            }
        }

        void InitPools()
        {
            applicationRequestPool = world.GetPool<ApplicationRequestComponent>();
            canBeAppliedFlagPool = world.GetPool<CanBeAppliedFlagComponent>();
        }
    }
}
