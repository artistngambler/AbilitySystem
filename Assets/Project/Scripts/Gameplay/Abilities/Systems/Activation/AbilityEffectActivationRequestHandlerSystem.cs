using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class AbilityEffectActivationRequestHandlerSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsFilter canBeActivatedFilter;
        EcsPool<ActivationRequestComponent> activationRequestPool;
        EcsPool<CanBeActivatedFlagComponent> canBeActivatedFlagPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<AbilityEffectComponent>().Inc<ActivationRequestComponent>().End();
            canBeActivatedFilter = world.Filter<AbilityEffectComponent>().Inc<CanBeActivatedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                activationRequestPool.Del(entity);
                canBeActivatedFlagPool.Add(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in canBeActivatedFilter)
            {
                canBeActivatedFlagPool.Del(entity);
            }
        }

        void InitPools()
        {
            activationRequestPool = world.GetPool<ActivationRequestComponent>();
            canBeActivatedFlagPool = world.GetPool<CanBeActivatedFlagComponent>();
        }
    }
}
