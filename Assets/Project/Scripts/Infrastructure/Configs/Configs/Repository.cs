namespace Configs
{
    public class Repository : IRepository
    {
        public GameComponents.Abilities.IConfig Abilities { get; } = new Abilities.Loader().Load();
    }
}
