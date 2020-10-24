using Entitas;

namespace DuckLib.Core.Blueprints
{
    public interface IBlueprint<TEntity> where TEntity : class, IEntity
    {
        void Apply(TEntity entity);
    }
}