using Leopotam.EcsLite;

namespace Gameplay.Abilities
{
    public class EffectFactory
    {
        readonly EcsWorld world;

        public EffectFactory(EcsWorld world)
        {
            this.world = world;
        }

        public EcsPackedEntity Create(AbilityEffect effect, EcsPackedEntity targetEntity)
        {
            var builder = new AbilityEffectBuilder(world, effect, targetEntity);

            return builder.Build();
        }

        public EcsPackedEntity Create(EffectModifier modifier, EcsPackedEntity targetEntity)
        {
            var builder = new EffectBuilder(world, modifier, targetEntity);

            return builder.Build();
        }
    }
}
