using DuckLib.Core.Blueprints;
using Entitas;

namespace DuckLib.Core.Extensions
{
    public static class ContextExtensions
    {
        public static void CreateEntity<TEntity>(this IContext<TEntity> context, IBlueprint<TEntity> blueprint)
            where TEntity : class, IEntity
        {
            blueprint.Apply(context.CreateEntity());
        }

        public static void Apply<TEntity>(this TEntity entity, IBlueprint<TEntity> blueprint)
            where TEntity : class, IEntity
        {
            blueprint.Apply(entity);
        }
    }
}