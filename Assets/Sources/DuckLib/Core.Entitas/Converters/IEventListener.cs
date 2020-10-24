using Entitas;

namespace DuckLib.Core.Converters
{
    public interface IEventListener<in TEntity> where TEntity : class, IEntity
    {
        void RegisterListeners(TEntity entity);
        void UnregisterListeners(TEntity with);
    }
}