namespace GameComponentRepositories
{
    public interface IRepository
    {
        GameComponents.Abilities.IRepository Abilities { get; }
    }
}
