using DuckLib.Core.Converters;
using UnityEngine;

namespace Gameplay.Game.View.Listeners
{
    public class PositionListener : MonoBehaviour, IPositionListener,
        IEventListener<GameEntity>
    {
        public void OnPosition(GameEntity entity, Vector3 value)
        {
            transform.position = value;
        }

        public void RegisterListeners(GameEntity entity)
        {
            Debug.Log("RegisterListeners");
            entity.AddPositionListener(this);
        }

        public void UnregisterListeners(GameEntity entity)
        {
            Debug.Log("UnregisterListeners");
            entity.RemovePositionListener(this);
        }
    }
}