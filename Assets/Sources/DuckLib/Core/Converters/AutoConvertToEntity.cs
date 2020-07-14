using Entitas;
using UnityEngine;
using Zenject;

namespace DuckLib.Core.Converters
{
    public interface IConvertToEntity<in TEntity> where TEntity : class, IEntity
    {
        void Convert(TEntity entity);
    }

    public abstract class AutoConvertToEntity<TEntity> : MonoBehaviour where TEntity : class, IEntity
    {
        private TEntity _entity;

        [Inject]
        public void Construct(IContext<TEntity> context)
        {
            _entity = context.CreateEntity();
            gameObject.Convert(_entity);
        }
    }
}