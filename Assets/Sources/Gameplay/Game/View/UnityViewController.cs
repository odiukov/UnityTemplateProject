using DuckLib.Core.Extensions;
using DuckLib.Core.View;
using Entitas.VisualDebugging.Unity;
using Gameplay.Game.View.Listeners;
using UnityEngine;

namespace Gameplay.Game.View
{
    public class UnityViewController : MonoBehaviour, IViewController<GameEntity>
    {
        public virtual void Convert(GameEntity entity)
        {
            Entity = entity;
            Entity.AddView(this);
            AddDestructedListener();
            OnStart();
        }

        protected virtual void OnStart()
        {

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

        public GameEntity Entity { get; private set; }
    }
}