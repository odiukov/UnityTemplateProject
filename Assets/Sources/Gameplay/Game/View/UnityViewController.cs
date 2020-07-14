using DuckLib.Core.Converters;
using DuckLib.Core.View;
using Entitas.VisualDebugging.Unity;
using Gameplay.Game.View.Listeners;
using UnityEngine;

namespace Gameplay.Game.View
{
    public class UnityViewController : MonoBehaviour, IViewController<GameEntity>
    {
        private GameEntity _gameEntity;

        public virtual void Convert(GameEntity entity)
        {
            _gameEntity = entity;
            _gameEntity.AddView(this);
            AddDestructedListener();
        }

        private void AddDestructedListener()
        {
            var destructedListener = GetComponent<DestructedListener>();
            if (destructedListener == null)
                gameObject.AddComponent<DestructedListener>();
        }

        public virtual void Destroy()
        {
            gameObject.UnregisterListeners(Entity);
            gameObject.DestroyGameObject();
        }

        public GameEntity Entity => _gameEntity;
    }
}