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
        }

        void RegisterGameComponents(IContainerBuilder builder)
        {
            builder.Register<GameComponents.IRepository, GameComponents.Repository>(Lifetime.Singleton);
        }
    }
}
