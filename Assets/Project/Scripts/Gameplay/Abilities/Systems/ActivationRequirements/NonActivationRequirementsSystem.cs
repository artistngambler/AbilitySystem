using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class NonActivationRequirementsSystem
        : IEcsInitSystem,
          IEcsRunSystem,
          IEcsPostRunSystem
    {
        EcsWorld world;
        EcsFilter filter;
        EcsFilter passedFilter;
        EcsPool<MeetActivationRequirementsComponent> meetActivationRequirementsPool;
        EcsPool<ActivationRequirementsCheckPassedFlagComponent> passedFlagPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<AbilityComponent>().Inc<NonActivationRequirementsComponent>()
               .Exc<MeetActivationRequirementsComponent>().End();

            passedFilter = world.Filter<AbilityComponent>().Inc<NonActivationRequirementsComponent>()
               .Inc<ActivationRequirementsCheckPassedFlagComponent>().End();

            InitPools();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter)
            {
                meetActivationRequirementsPool.Add(entity);
                passedFlagPool.Add(entity);
            }
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int entity in passedFilter)
            {
                passedFlagPool.Del(entity);
            }
        }

        void InitPools()
        {
            meetActivationRequirementsPool = world.GetPool<MeetActivationRequirementsComponent>();
            passedFlagPool = world.GetPool<ActivationRequirementsCheckPassedFlagComponent>();
        }
    }
}
