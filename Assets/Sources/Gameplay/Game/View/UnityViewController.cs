using DuckLib.Core.View;
using UnityEngine;

namespace Gameplay.Game.View
{
    public class UnityViewController : MonoBehaviour, IViewController<GameEntity>
    {
        private GameEntity _gameEntity;

        public void Convert(GameEntity entity)
        {
            _gameEntity = entity;
            _gameEntity.AddView(this);
        }

        public virtual void Destroy()
        {
        }

        public GameEntity Entity => _gameEntity;
    }
}