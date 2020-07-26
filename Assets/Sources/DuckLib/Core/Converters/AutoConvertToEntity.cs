using DuckLib.Core.Extensions;
using DuckLib.Core.View;
using Entitas;
using UnityEngine;
using Zenject;

namespace DuckLib.Core.Converters
{
    public interface IConvertToEntity<in TEntity> where TEntity : class, IEntity
    {
        void Convert(TEntity entity);
    }

    public abstract class AutoConvertToEntity<TController, TEntity> : MonoBehaviour where TEntity : class, IEntity
        where TController : Component, IViewController<TEntity>
    {
        private TEntity _entity;

        [Inject]
        public void Construct(IContext<TEntity> context)
        {
            _entity = context.CreateEntity();
            gameObject
                .CreateController<TController, TEntity>()
                .ConvertGameObjectToEntity(_entity);
        }
    }
}