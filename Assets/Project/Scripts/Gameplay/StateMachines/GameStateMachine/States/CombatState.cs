using Gameplay.Abilities;
using Gameplay.Modifiers;
using Leopotam.EcsLite;
using System;

namespace Gameplay.StateMachines.GameSM
{
    public class CombatState : IState
    {
        readonly EcsWorld world;
        readonly GameComponents.IRepository gameComponents;
        readonly Random random;
        readonly MagnitudeCalculatorsRepository magnitudeCalculatorsRepository;

        EcsSystems systems;

        public CombatState(EcsWorld world, GameComponents.IRepository gameComponents, Random random)
        {
            this.world = world;
            this.gameComponents = gameComponents;
            this.random = random;
            magnitudeCalculatorsRepository = new MagnitudeCalculatorsRepository(random);
        }

        public void Enter()
        {
            systems = new EcsSystems(world);

            systems
               .Add(new NonActivationRequirementsSystem())
               .Add(new ActivationRequirementsCheckSystem())

               .Add(new PassiveAbilityCancellationRequestSenderSystem())
               .Add(new PassiveAbilityActivationRequestSenderSystem())

               .Add(new AbilityActivationRequestHandlerSystem())

               .Add(new AbilityApplicationRequestHandlerSystem(random))
               .Add(new AbilityEffectActivationRequestHandlerSystem())

               .Add(new InstantAbilityEffectActivationSystem())
               .Add(new InfiniteAbilityEffectActivationSystem())
               .Add(new DurationTimerSystem())
               .Add(new CancelAbilityEffectByFinishDurationRequestSenderSystem())
               .Add(new HasDurationAbilityEffectActivationSystem())

               .Add(new AbilityEffectCancellationRequestHandlerSystem())
               .Add(new AbilityEffectApplicationRequestHandlerSystem())
               .Add(new AbilityEffectCancellationSystem())
               .Add(new AbilityEffectApplicationSystem(magnitudeCalculatorsRepository))
               .Add(new EffectApplicationSystem(magnitudeCalculatorsRepository));

            systems.Init();
        }

        public void Exit()
        {
            if (systems == null)
            {
                return;
            }

            systems.Destroy();
            systems = null;
        }

        public void Execute()
        {
            systems?.Run();
        }
    }
}
