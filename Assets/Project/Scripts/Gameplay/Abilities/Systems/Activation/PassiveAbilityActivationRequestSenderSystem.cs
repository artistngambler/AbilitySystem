using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class PassiveAbilityActivationRequestSenderSystem
        : IEcsInitSystem,
          IEcsRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsPool<ActivationRequestComponent> activationRequestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<PassiveAbilityComponent>().Inc<ActivationRequirementsCheckPassedFlagComponent>()
               .Exc<ActivationRequestComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                activationRequestPool.Add(entity);
            }
        }

        void InitPools()
        {
            activationRequestPool = world.GetPool<ActivationRequestComponent>();
        }
    }
}
