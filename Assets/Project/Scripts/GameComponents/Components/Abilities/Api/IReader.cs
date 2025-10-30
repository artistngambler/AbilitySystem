using GameComponents.Abilities.ExternalTypes;

namespace GameComponents.Abilities
{
    public interface IReader
    {
        Ability Get(string id);
    }
}
