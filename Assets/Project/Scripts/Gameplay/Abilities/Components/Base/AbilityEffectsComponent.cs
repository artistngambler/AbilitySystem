using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Gameplay.Abilities
{
    public struct AbilityEffectsComponent
    {
        public List<AbilityEffect> Effects;
        public List<EcsPackedEntity> EffectEntities;
    }
}
