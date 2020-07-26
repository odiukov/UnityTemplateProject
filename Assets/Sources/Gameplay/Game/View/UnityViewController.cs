using System;
using DuckLib.Core.Extensions;
using DuckLib.Core.Services;
using DuckLib.Core.View;
using Entitas.VisualDebugging.Unity;
using Gameplay.Common.Contexts;
using Gameplay.Game.Extensions;
using Gameplay.Game.View.Listeners;
using UnityEngine;
using Zenject;

namespace Gameplay.Game.View
{
    public class UnityViewController : MonoBehaviour, IViewController<GameEntity>
    {
        private IRegisterService<IViewController<GameEntity>> _collidingViewRegister;
        private IIdentifierService _identifierService;

        [Inject]
        public void Construct(IGameContext context, IRegisterService<IViewController<GameEntity>> collidingViewRegister,
            IIdentifierService identifierService)
        {
            _collidingViewRegister = collidingViewRegister;
            _identifierService = identifierService;
        }

        public virtual void Convert(GameEntity entity)
        {
            Entity = entity;
            Entity.AddView(this);
            AddDestructedListener();
        }

        private void Start()
        {
            RegisterCollisions();
            Entity.WithNewGeneralId(_identifierService);
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
            UnregisterCollisions();
            gameObject.UnregisterListeners(Entity);
            gameObject.DestroyGameObject();
        }

        private void RegisterCollisions()
        {
            foreach (var collider in GetComponentsInChildren<Collider2D>(true))
                _collidingViewRegister.Register(collider.GetInstanceID(), this);
        }

        private void UnregisterCollisions()
        {
            foreach (var collider in GetComponentsInChildren<Collider2D>())
                _collidingViewRegister.Unregister(collider.GetInstanceID(), this);
        }

        public GameEntity Entity { get; private set; }
    }
}