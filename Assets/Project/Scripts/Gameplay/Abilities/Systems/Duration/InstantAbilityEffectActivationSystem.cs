using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class InstantAbilityEffectActivationSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsFilter onDestroyFilter;
        EcsPool<ApplicationRequestComponent> applicationRequestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<AbilityEffectComponent>().Inc<InstantComponent>().Inc<CanBeActivatedFlagComponent>()
               .Exc<ApplicationRequestComponent>().End();

            onDestroyFilter = world.Filter<AbilityEffectComponent>().Inc<InstantComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                applicationRequestPool.Add(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in onDestroyFilter)
            {
                world.DelEntity(entity);
            }
        }

        void InitPools()
        {
            applicationRequestPool = world.GetPool<ApplicationRequestComponent>();
        }
    }
}
