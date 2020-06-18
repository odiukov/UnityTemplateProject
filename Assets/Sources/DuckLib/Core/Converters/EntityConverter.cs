using Entitas;
using UnityEngine;

namespace DuckLib.Core.Converters
{
    public static class EntityConverter
    {
        public static void Convert<TEntity>(this GameObject viewGo, TEntity entity) where TEntity : class, IEntity
        {
            var wrappers = viewGo.GetComponents<IConvertToEntity<TEntity>>();
            foreach (var eventListener in wrappers)
            {
                eventListener.Convert(entity);
            }
        }

        public static void RegisterListeners<TEntity>(this GameObject viewGo, TEntity entity) where TEntity : class, IEntity
        {
            var eventListeners = viewGo.GetComponents<IEventListener<TEntity>>();
            foreach (var eventListener in eventListeners)
            {
                eventListener.RegisterListeners(entity);
            }
        }

        public static void UnregisterListeners<TEntity>(this GameObject viewGo, TEntity entity) where TEntity : class, IEntity
        {
            var eventListeners = viewGo.GetComponents<IEventListener<TEntity>>();
            foreach (var eventListener in eventListeners)
            {
                eventListener.UnregisterListeners(entity);
            }
        }
    }
}