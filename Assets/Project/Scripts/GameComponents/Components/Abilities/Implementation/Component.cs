namespace GameComponents.Abilities
{
    public class Component : IComponent
    {
        public IReader Reader { get; }

        public Component(IRepository repository)
        {
            Reader = new Reader(repository);
        }
    }
}
