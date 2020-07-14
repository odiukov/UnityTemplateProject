using DuckLib.Core.Converters;
using Entitas;

namespace DuckLib.Core.View
{
    public interface IViewController<TEntity> : IConvertToEntity<TEntity> where TEntity : class, IEntity
    {
        void Destroy();
        TEntity Entity { get; }
    }
}