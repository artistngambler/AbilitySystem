using VContainer;

namespace GameComponents
{
    public class Repository : IRepository
    {
        public Abilities.IComponent Abilities { get; }
        
        public Repository(IObjectResolver resolver)
        {
            Abilities = resolver.Resolve<Abilities.IComponent>();
        }
    }
}
