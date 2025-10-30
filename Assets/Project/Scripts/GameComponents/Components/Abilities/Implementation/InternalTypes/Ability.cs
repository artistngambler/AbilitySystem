namespace GameComponents.Abilities.InternalTypes
{
    public class Ability
    {
        public readonly string Id;
        public readonly AbilityType Type;
        public readonly ActivationRequirements ActivationRequirements;
        public readonly Cast Cast;
        public readonly Cooldown Cooldown;
        public readonly Effect[] Effects;

        public Ability(string id, AbilityType type, ActivationRequirements activationRequirements, Cast cast, Cooldown cooldown,
            Effect[] effects)
        {
            Id = id;
            Type = type;
            ActivationRequirements = activationRequirements;
            Cast = cast;
            Cooldown = cooldown;
            Effects = effects;
        }
    }
}
