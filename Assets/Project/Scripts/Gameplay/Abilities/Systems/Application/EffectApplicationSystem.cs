using Attributes;
using GameComponents.Abilities;
using Gameplay.Common;
using Gameplay.Modifiers;
using Leopotam.EcsLite;
using System;

namespace Gameplay.Abilities
{
    public class EffectApplicationSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        readonly MagnitudeCalculatorsRepository magnitudeCalculatorsRepository;

        EcsWorld world;
        EcsFilter filter;
        EcsPool<TargetComponent> targetPool;
        EcsPool<AttributeSetComponent> attributeSetPool;
        EcsPool<EffectModifierComponent> effectModifierPool;

        public EffectApplicationSystem(MagnitudeCalculatorsRepository magnitudeCalculatorsRepository)
        {
            this.magnitudeCalculatorsRepository = magnitudeCalculatorsRepository;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<EffectComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                TargetComponent targetComponent = targetPool.Get(entity);
                AttributeSetComponent attributeSetComponent = attributeSetPool.Get(targetComponent.Value.Id);
                EffectModifierComponent modifierComponent = effectModifierPool.Get(entity);

                IMagnitudeCalculator magnitudeCalculator =
                    magnitudeCalculatorsRepository.GetIncomeMagnitudeCalculator(modifierComponent.MagnitudeCalculationClass);

                magnitudeCalculator.SetAttributes(attributeSetComponent.Value);

                float[] parameters = { modifierComponent.Magnitude };
                magnitudeCalculator.SetParameters(parameters);

                float magnitude = magnitudeCalculator.Calculate();
                GameplayAttribute attribute = attributeSetComponent.Value.Get(modifierComponent.Attribute);

                Modify(attribute, modifierComponent.Operation, magnitude);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                world.DelEntity(entity);
            }
        }

        void Modify(GameplayAttribute attribute, ModifyOperation operation, float magnitude)
        {
            switch (operation)
            {
                case ModifyOperation.Add:
                    attribute.AddAdditive(magnitude);
                    break;

                case ModifyOperation.Multiply:
                    attribute.AddMultiplicative(magnitude);
                    break;

                default:
                    throw new Exception($"<EffectApplicationSystem::Modify>: Unknown modify operation {operation}>");
            }
        }

        void InitPools()
        {
            targetPool = world.GetPool<TargetComponent>();
            attributeSetPool = world.GetPool<AttributeSetComponent>();
            effectModifierPool = world.GetPool<EffectModifierComponent>();
        }
    }
}
