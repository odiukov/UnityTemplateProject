using System;
using DuckLib.Core.Converters;
using DuckLib.Core.Extensions;
using DuckLib.Core.View;
using UnityEngine;

namespace Gameplay.Game.View.Listeners
{
    public class DestructedListener : MonoBehaviour, IEventListener<GameEntity>, IDestructListener
    {
        private GameEntity _entity;

        public void RegisterListeners(GameEntity entity)
        {
            _entity = entity;
            _entity.AddDestructListener(this);
            OnDestruct(_entity);
        }

        public void OnDestruct(GameEntity entity) =>
            this.Do(Cleanup(entity), when: entity.isDestruct);

        private Action<DestructedListener> Cleanup(GameEntity entity) =>
            listener =>
            {
                var controller = gameObject.GetComponent<IViewController<GameEntity>>();
                controller.Destroy();
            };

        public void UnregisterListeners(GameEntity with) => _entity.RemoveDestructListener();
    }
}