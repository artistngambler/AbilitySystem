using Gameplay;
using System;
using VContainer.Unity;

namespace UnityBridge
{
    public class EntryPoint
        : IStartable,
          ITickable,
          IDisposable
    {
        readonly GameComponents.IRepository gameComponents;

        World world;

        public EntryPoint(GameComponents.IRepository gameComponents)
        {
            this.gameComponents = gameComponents;
        }

        public void Dispose()
        {
            world.Cleanup();
        }

        public void Start()
        {
            world = new World(gameComponents);
            world.Initialize();
        }

        public void Tick()
        {
            world.Execute();
        }
    }
}
