using Gameplay.Common;
using Leopotam.EcsLite;
using Math;
using System;

namespace Gameplay.Abilities
{
    public class AbilityApplicationRequestHandlerSystem
        : IEcsInitSystem,
          IEcsRunSystem
    {
        readonly Random random;

        EcsWorld world;
        EcsFilter filter;
        EcsPool<ApplicationRequestComponent> applicationRequestPool;
        EcsPool<ActivationRequestComponent> activationRequestPool;
        EcsPool<AbilityEffectsComponent> abilityEffectsPool;
        EcsPool<TargetComponent> targetPool;
        EcsPool<PassiveAbilityComponent> passiveAbilityPool;

        EffectFactory effectFactory;

        public AbilityApplicationRequestHandlerSystem(Random random)
        {
            this.random = random;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<AbilityComponent>().Inc<ApplicationRequestComponent>().End();

            InitPools();

            effectFactory = new EffectFactory(world);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                applicationRequestPool.Del(entity);

                AbilityEffectsComponent effectsComponent = abilityEffectsPool.Get(entity);
                TargetComponent targetComponent = targetPool.Get(entity);

                ApplyEffectsToTarget(ref effectsComponent, targetComponent.Value);
            }
        }

        void ApplyEffectsToTarget(ref AbilityEffectsComponent effectsComponent, EcsPackedEntity targetEntity)
        {
            foreach (AbilityEffect abilityEffect in effectsComponent.Effects)
            {
                bool success = ProbabilityChecker.Check(abilityEffect.Rate, random);

                if (!success)
                {
                    return;
                }

                EcsPackedEntity effectEntity = effectFactory.Create(abilityEffect, targetEntity);
                activationRequestPool.Add(effectEntity.Id);

                if (passiveAbilityPool.Has(effectEntity.Id))
                {
                    effectsComponent.EffectEntities.Add(effectEntity);
                }
            }
        }

        void InitPools()
        {
            applicationRequestPool = world.GetPool<ApplicationRequestComponent>();
            activationRequestPool = world.GetPool<ActivationRequestComponent>();
            abilityEffectsPool = world.GetPool<AbilityEffectsComponent>();
            targetPool = world.GetPool<TargetComponent>();
            passiveAbilityPool = world.GetPool<PassiveAbilityComponent>();
        }
    }
}
