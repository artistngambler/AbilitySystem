using Gameplay.StateMachines;
using Gameplay.StateMachines.GameSM;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class World
    {
        readonly GameStateMachine gameStateMachine;

        EcsWorld world;

        public World(GameComponents.IRepository gameComponents)
        {
            world = new EcsWorld();
            var random = new Random(DateTime.UtcNow.Millisecond);

            var states = new List<IState> { new CombatState(world, gameComponents, random) };
            gameStateMachine = new GameStateMachine(states);
        }

        public void Initialize()
        {
            gameStateMachine.SwitchState<CombatState>();
        }

        public void Execute()
        {
            gameStateMachine.Execute();
        }

        public void Cleanup()
        {
            gameStateMachine.Cleanup();
            world.Destroy();
            world = null;
        }
    }
}
