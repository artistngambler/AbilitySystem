using GameComponents.Abilities;
using GameComponents.Abilities.ExternalTypes;
using Leopotam.EcsLite;
using System;

namespace Gameplay.Abilities
{
    public class AbilityFactory
    {
        readonly EcsWorld world;

        public AbilityFactory(EcsWorld world)
        {
            this.world = world;
        }

        public EcsPackedEntity Create(Ability ability, EcsPackedEntity sourceEntity)
        {
            switch (ability.Type)
            {
                case AbilityType.Active:
                    var activeAbilityBuilder = new ActiveAbilityBuilder(world, ability, sourceEntity);

                    return activeAbilityBuilder.Build();

                case AbilityType.Passive:
                    var passiveAbilityBuilder = new PassiveAbilityBuilder(world, ability, sourceEntity);

                    return passiveAbilityBuilder.Build();

                default:
                    throw new Exception($"<AbilityFactory::Create>: Unknown ability type: {ability.Type}");
            }
        }
    }
}
