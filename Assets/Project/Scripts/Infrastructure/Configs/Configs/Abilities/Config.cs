using GameComponents.Abilities;
using GameComponents.Abilities.InternalTypes;
using System.Collections.Generic;

namespace Configs.Abilities
{
    public class Config : IConfig
    {
        public Dictionary<string, Ability> Abilities { get; }

        public Config(Dictionary<string, Ability> abilities)
        {
            Abilities = abilities;
        }
    }
}
