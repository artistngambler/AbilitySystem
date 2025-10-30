using GameComponents.Abilities.InternalTypes;
using System.Collections.Generic;

namespace GameComponents.Abilities
{
    public interface IConfig
    {
        Dictionary<string, Ability> Abilities { get; }
    }
}
