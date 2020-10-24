using System;
using System.Linq;
using DuckLib.Core.Attributes;
using DuckLib.Core.Converters;
using DuckLib.Core.View;
using Entitas;
using UnityEngine;
using Zenject;

namespace DuckLib.Core.Extensions
{
    public static class EntityConverterExtensions
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

        public static GameObject RegisterListeners<TEntity>(this GameObject viewGo, TEntity with)
            where TEntity : class, IEntity
        {
            var eventListeners = viewGo.GetComponents<IEventListener<TEntity>>();
            foreach (var eventListener in eventListeners)
            {
                eventListener.RegisterListeners(with);
            }

            return viewGo;
        }

        public static GameObject UnregisterListeners<TEntity>(this GameObject viewGo, TEntity with)
            where TEntity : class, IEntity
        {
            var eventListeners = viewGo.GetComponents<IEventListener<TEntity>>();
            foreach (var eventListener in eventListeners)
            {
                eventListener.UnregisterListeners(with);
            }

            return viewGo;
        }

        public static GameObject CreateController<T, TEntity>(this GameObject view, DiContainer container)
            where T : Component, IViewController<TEntity>
            where TEntity : class, IEntity
        {
            if (view.GetComponent<T>() == null)
                container.InstantiateComponent<T>(view);
            return view;
        }

        public static void ConvertGameObjectToEntity<TEntity>(this GameObject view, TEntity entity)
            where TEntity : class, IEntity
        {
            view
                .Convert(with: entity)
                .AddListeners(@from: entity)
                .RegisterListeners(with: entity);
        }

        private static GameObject AddListeners<TEntity>(this GameObject view, TEntity @from)
            where TEntity : class, IEntity
        {
            foreach (var component in @from.GetComponents().Select(c => c.GetType()))
            {
                // TODO: replace it. slow reflection
                var attributes = Attribute.GetCustomAttributes(component);
                foreach (var attribute in attributes)
                {
                    if (attribute is AutoListenerAttribute atr)
                    {
                        view.AddComponent(atr.Type);
                    }
                }
            }

            return view;
        }
    }
}