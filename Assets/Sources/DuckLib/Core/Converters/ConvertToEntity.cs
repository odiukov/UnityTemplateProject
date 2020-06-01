using Entitas;
using Entitas.Unity;
using UnityEngine;
using Zenject;

namespace DuckLib.Core.Converters
{
    public interface IConvertToEntity<in TEntity>
    {
        void Convert(TEntity entity);
    }

    public abstract class ConvertToEntity<TEntity> : MonoBehaviour where TEntity : class, IEntity
    {
        [Inject]
        public void Construct(IContext<TEntity> context)
        {
            var gameEntity = context.CreateEntity();
            gameObject.Convert(gameEntity);
        }
    }
}