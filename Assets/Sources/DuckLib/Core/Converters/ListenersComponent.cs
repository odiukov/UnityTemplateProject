using DuckLib.Core.View;
using Entitas;
using UnityEngine;

namespace DuckLib.Core.Converters
{
    public abstract class ListenersComponent<TEntity> : MonoBehaviour where TEntity : class, IEntity
    {
        private TEntity _entity;

        public void Start()
        {
            var viewController = GetComponent<IViewController<TEntity>>();
            if (viewController != null)
            {
                _entity = viewController.Entity;
                gameObject.RegisterListeners(_entity);
            }
            else
            {
                Debug.LogError("Object should contains IViewController component");
            }
        }

        private void OnDestroy()
        {
            if(_entity != null)
                gameObject.UnregisterListeners(_entity);
        }
    }
}