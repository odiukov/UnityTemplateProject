using DuckLib.Core.Blueprints;
using Entitas;

namespace DuckLib.Core.Extensions
{
    public static class ContextExtensions
    {
        public static TEntity CreateEntity<TEntity>(this IContext<TEntity> context, IBlueprint<TEntity> blueprint)
            where TEntity : class, IEntity
        {
            return context.CreateEntity().Apply(blueprint);
        }

        public static TEntity Apply<TEntity>(this TEntity entity, IBlueprint<TEntity> blueprint)
            where TEntity : class, IEntity
        {
            blueprint.Apply(entity);
            return entity;
        }
    }
}