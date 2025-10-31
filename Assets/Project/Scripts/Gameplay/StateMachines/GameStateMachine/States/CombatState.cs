using Leopotam.EcsLite;

namespace Gameplay.StateMachines.GameSM
{
    public class CombatState : IState
    {
        readonly EcsWorld world;
        readonly GameComponents.IRepository gameComponents;

        EcsSystems systems;

        public CombatState(EcsWorld world, GameComponents.IRepository gameComponents)
        {
            this.world = world;
            this.gameComponents = gameComponents;
        }

        public void Enter()
        {
            systems = new EcsSystems(world);
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
