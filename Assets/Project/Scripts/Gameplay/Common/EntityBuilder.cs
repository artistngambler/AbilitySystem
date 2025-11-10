using Leopotam.EcsLite;

namespace Gameplay.Common
{
    public class EntityBuilder
    {
        readonly EcsWorld world;
        readonly int entity;
        readonly EcsPackedEntity packedEntity;

        public EntityBuilder(EcsWorld world)
        {
            this.world = world;
            entity = world.NewEntity();
            packedEntity = world.PackEntity(entity);
        }

        public EntityBuilder With<TComponent>(TComponent component)
            where TComponent : struct
        {
            EcsPool<TComponent> pool = world.GetPool<TComponent>();
            ref TComponent entityComponent = ref pool.Add(entity);
            entityComponent = component;
            
            return this;
        }

        public EcsPackedEntity Build()
        {
            return packedEntity;
        }
    }
}
