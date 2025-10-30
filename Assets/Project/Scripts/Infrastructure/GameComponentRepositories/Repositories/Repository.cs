using VContainer;

namespace GameComponentRepositories
{
    public class Repository : IRepository
    {
        public GameComponents.Abilities.IRepository Abilities { get; }

        public Repository(IObjectResolver resolver)
        {
            Abilities = resolver.Resolve<GameComponents.Abilities.IRepository>();
        }
    }
}
