using GameComponents.Abilities;
using GameComponents.Abilities.InternalTypes;
using System;
using System.Collections.Generic;

namespace GameComponentRepositories.Abilities
{
    public class Repository : GameComponents.Abilities.IRepository
    {
        readonly Dictionary<string, Ability> abilities;
        
        public Repository(IConfig config)
        {
            abilities = config.Abilities;
        }

        public Ability Get(string id)
        {
            if (!abilities.ContainsKey(id))
            {
                throw new Exception($"<AbilitiesRepository::Get>: Abilities with ID {id} not found.");
            }
            
            return abilities[id];
        }
    }
}
