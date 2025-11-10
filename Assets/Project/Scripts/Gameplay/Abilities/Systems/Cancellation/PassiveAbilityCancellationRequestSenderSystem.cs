using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class PassiveAbilityCancellationRequestSenderSystem
        : IEcsInitSystem,
          IEcsRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsPool<CancellationRequestComponent> cancellationRequestPool;
        EcsPool<AbilityEffectsComponent> abilityEffectsPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<PassiveAbilityComponent>().Inc<ActivationRequirementsCheckFailedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                ref AbilityEffectsComponent abilityEffectsComponent = ref abilityEffectsPool.Get(entity);

                foreach (EcsPackedEntity abilityEffect in abilityEffectsComponent.EffectEntities)
                {
                    if (!cancellationRequestPool.Has(abilityEffect.Id))
                    {
                        cancellationRequestPool.Add(abilityEffect.Id);
                    }
                }

                abilityEffectsComponent.EffectEntities.Clear();
            }
        }

        void InitPools()
        {
            cancellationRequestPool = world.GetPool<CancellationRequestComponent>();
            abilityEffectsPool = world.GetPool<AbilityEffectsComponent>();
        }
    }
}
