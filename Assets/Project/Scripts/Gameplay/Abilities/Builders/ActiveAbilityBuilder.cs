using GameComponents.Abilities;
using GameComponents.Abilities.ExternalTypes;
using Gameplay.Common;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Gameplay.Abilities
{
    public class ActiveAbilityBuilder
    {
        readonly EntityBuilder entityBuilder;

        public ActiveAbilityBuilder(EcsWorld world, Ability ability, EcsPackedEntity sourceEntity)
        {
            entityBuilder = new EntityBuilder(world);

            var idComponent = new IdComponent { Value = ability.Id };
            var sourceComponent = new SourceComponent { Value = sourceEntity };
            var abilityComponent = new AbilityComponent();
            var activeAbilityComponent = new ActiveAbilityComponent();

            entityBuilder.With(idComponent).With(sourceComponent).With(abilityComponent).With(activeAbilityComponent);

            AddEffects(ability.Effects);
            AddActivationRequirements(ability.ActivationRequirements);
            AddCast(ability.Cast);
            AddCooldown(ability.Cooldown);
        }

        public EcsPackedEntity Build()
        {
            return entityBuilder.Build();
        }

        void AddEffects(Effect[] effects)
        {
            if (effects == null
             || effects.Length == 0)
            {
                return;
            }

            var component = new AbilityEffectsComponent
            {
                Effects = new List<AbilityEffect>(),
                EffectEntities = new List<EcsPackedEntity>()
            };

            foreach (Effect effect in effects)
            {
                var abilityEffect = effect.ToAbilityEffect();
                component.Effects.Add(abilityEffect);
            }

            entityBuilder.With(component);
        }

        void AddActivationRequirements(ActivationRequirements requirements)
        {
            if (requirements == null)
            {
                entityBuilder.With(new NonActivationRequirementsComponent());

                return;
            }

            var component = new ActivationRequirementsComponent
            {
                Attribute = requirements.Attribute,
                Operation = requirements.Operation,
                Value = requirements.Value,
                ValueType = requirements.ValueType
            };

            entityBuilder.With(component);
        }

        void AddCast(Cast cast)
        {
            if (cast == null)
            {
                entityBuilder.With(new NonCastComponent());

                return;
            }

            var component = new CastComponent { Value = cast.Value };
            entityBuilder.With(component);
        }

        void AddCooldown(Cooldown cooldown)
        {
            if (cooldown == null)
            {
                entityBuilder.With(new NonCooldownComponent());

                return;
            }

            var component = new CooldownComponent { Value = cooldown.Value };
            entityBuilder.With(component);
        }
    }
}
