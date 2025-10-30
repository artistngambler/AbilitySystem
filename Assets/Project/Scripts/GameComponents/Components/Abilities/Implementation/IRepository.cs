namespace GameComponents.Abilities
{
    public interface IRepository
    {
        InternalTypes.Ability Get(string id);
    }
}
