using JsonSerialization;
using VContainer;
using VContainer.Unity;

namespace UnityBridge.DiRegistration
{
    public class ProjectScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            InitializeJson();
            RegisterConfigs(builder);
            RegisterGameComponentRepositories(builder);
            RegisterGameComponents(builder);
            builder.RegisterEntryPoint<EntryPoint>();
        }

        void InitializeJson()
        {
            JsonSerializer.Initialize();
        }

        void RegisterConfigs(IContainerBuilder builder)
        {
            builder.Register<Configs.IRepository, Configs.Repository>(Lifetime.Singleton);
            builder.Register(c => c.Resolve<Configs.IRepository>().Abilities, Lifetime.Singleton);
        }

        void RegisterGameComponentRepositories(IContainerBuilder builder)
        {
            builder.Register<GameComponents.Abilities.IRepository, GameComponentRepositories.Abilities.Repository>(
                Lifetime.Singleton
            );

            builder.Register<GameComponentRepositories.IRepository, GameComponentRepositories.Repository>(Lifetime.Singleton);
        }

        void RegisterGameComponents(IContainerBuilder builder)
        {
            RegisterAbilities(builder);

            builder.Register<GameComponents.IRepository, GameComponents.Repository>(Lifetime.Singleton);
        }

        void RegisterAbilities(IContainerBuilder builder)
        {
            builder.Register<GameComponents.Abilities.IComponent, GameComponents.Abilities.Component>(Lifetime.Singleton);
            builder.Register(c => c.Resolve<GameComponents.Abilities.IComponent>().Reader, Lifetime.Singleton);
        }
    }
}
