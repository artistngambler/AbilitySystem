using Attributes;
using GameComponents.Abilities;
using Gameplay.Common;
using Leopotam.EcsLite;
using System;
using Attribute = Attributes.Attribute;

namespace Gameplay.Abilities
{
    public class ActivationRequirementsCheckSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsFilter passedFilter;
        EcsFilter failedFilter;
        EcsPool<ActivationRequirementsComponent> activationRequirementsPool;
        EcsPool<SourceComponent> sourcePool;
        EcsPool<AttributeSetComponent> attributeSetPool;
        EcsPool<MeetActivationRequirementsComponent> meetActivationRequirementsPool;
        EcsPool<NotMeetActivationRequirementsComponent> notMeetActivationRequirementsPool;
        EcsPool<ActivationRequirementsCheckPassedFlagComponent> passedFlagPool;
        EcsPool<ActivationRequirementsCheckFailedFlagComponent> failedFlagPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<AbilityComponent>().Inc<ActivationRequirementsComponent>().End();

            passedFilter = world.Filter<AbilityComponent>().Inc<ActivationRequirementsComponent>()
               .Inc<ActivationRequirementsCheckPassedFlagComponent>().End();

            failedFilter = world.Filter<AbilityComponent>().Inc<ActivationRequirementsComponent>()
               .Inc<ActivationRequirementsCheckFailedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                ActivationRequirementsComponent requirementsComponent = activationRequirementsPool.Get(entity);
                SourceComponent sourceComponent = sourcePool.Get(entity);
                AttributeSetComponent attributeSetComponent = attributeSetPool.Get(sourceComponent.Value.Id);

                bool passed = Check(ref requirementsComponent, attributeSetComponent.Value);

                if (passed)
                {
                    if (meetActivationRequirementsPool.Has(entity))
                    {
                        continue;
                    }

                    meetActivationRequirementsPool.Add(entity);
                    passedFlagPool.Add(entity);
                    notMeetActivationRequirementsPool.Del(entity);
                }
                else
                {
                    if (notMeetActivationRequirementsPool.Has(entity))
                    {
                        continue;
                    }

                    notMeetActivationRequirementsPool.Add(entity);
                    failedFlagPool.Add(entity);
                    meetActivationRequirementsPool.Del(entity);
                }
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in passedFilter)
            {
                passedFlagPool.Del(entity);
            }

            foreach (int entity in failedFilter)
            {
                failedFlagPool.Del(entity);
            }
        }

        bool Check(ref ActivationRequirementsComponent requirementsComponent, AttributeSet sourceAttributes)
        {
            float value;
            float targetValue;

            switch (requirementsComponent.ValueType)
            {
                case CompareValueType.Absolute:
                    value = requirementsComponent.Value;
                    targetValue = sourceAttributes.Get(requirementsComponent.Attribute).Value;

                    break;

                case CompareValueType.Relative:
                    value = sourceAttributes.Get(requirementsComponent.Attribute).Value;

                    targetValue = requirementsComponent.Attribute switch
                    {
                        Attribute.Health => sourceAttributes.Get(Attribute.MaxHealth).Value * requirementsComponent.Value / 100f,
                        _ => 0f,
                    };

                    break;

                default:
                    throw new Exception(
                        $"<ActivationRequirementsCheckSystem::Check>: Invalid compare value type {requirementsComponent.ValueType}"
                    );
            }

            return Comparer.Compare(requirementsComponent.Operation, value, targetValue);
        }

        void InitPools()
        {
            activationRequirementsPool = world.GetPool<ActivationRequirementsComponent>();
            sourcePool = world.GetPool<SourceComponent>();
            attributeSetPool = world.GetPool<AttributeSetComponent>();
            meetActivationRequirementsPool = world.GetPool<MeetActivationRequirementsComponent>();
            notMeetActivationRequirementsPool = world.GetPool<NotMeetActivationRequirementsComponent>();
            passedFlagPool = world.GetPool<ActivationRequirementsCheckPassedFlagComponent>();
            failedFlagPool = world.GetPool<ActivationRequirementsCheckFailedFlagComponent>();
        }
    }
}
