using Entitas;
using UnityEngine;

namespace DuckLib.Core.Converters
{
    public static class EntityConverter
    {
        public static GameObject Convert<TEntity>(this GameObject viewGo, TEntity with) where TEntity : class, IEntity
        {
            var wrappers = viewGo.GetComponents<IConvertToEntity<TEntity>>();
            foreach (var eventListener in wrappers)
            {
                eventListener.Convert(with);
            }
            return viewGo;
        }

        public static GameObject RegisterListeners<TEntity>(this GameObject viewGo, TEntity with) where TEntity : class, IEntity
        {
            var eventListeners = viewGo.GetComponents<IEventListener<TEntity>>();
            foreach (var eventListener in eventListeners)
            {
                eventListener.RegisterListeners(with);
            }
            return viewGo;
        }

        public static GameObject UnregisterListeners<TEntity>(this GameObject viewGo, TEntity with) where TEntity : class, IEntity
        {
            var eventListeners = viewGo.GetComponents<IEventListener<TEntity>>();
            foreach (var eventListener in eventListeners)
            {
                eventListener.UnregisterListeners(with);
            }
            return viewGo;
        }
    }
}