using DuckLib.Core.Converters;
using DuckLib.Core.Extensions;
using DuckLib.Core.Services;
using DuckLib.Core.View;
using Gameplay.Game.Extensions;
using UnityEngine;
using Zenject;

namespace Gameplay.Game.View.Wrappers
{
    public sealed class TriggerOnEnter2D : MonoBehaviour, IConvertToEntity<GameEntity>
    {
        [SerializeField] private LayerMask triggeringLayers;

        private GameEntity _entity;
        private IRegisterService<IViewController<GameEntity>> _collidingViewRegister;

        [Inject]
        public void Construct(IRegisterService<IViewController<GameEntity>> collidingViewRegister)
        {
            _collidingViewRegister = collidingViewRegister;
        }

        public void Convert(GameEntity entity)
        {
            _entity = entity;
        }

        private void OnTriggerEnter2D(Collider2D collision) => TriggerBy(collision);

        private void TriggerBy(Collider2D collision)
        {
            if (_entity.isCollided || !collision.Matches(triggeringLayers))
                return;

            var entered = _collidingViewRegister
                .Take(collision.GetInstanceID())
                .Entity;

            _entity.MarkCollided(@by: entered.id.Value);
            entered.MarkCollided(@by: _entity.id.Value);
        }
    }
}