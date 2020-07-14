using System;
using System.Collections.Generic;
using System.Linq;
using DuckLib.Core.Converters;
using DuckLib.Core.Extensions;
using DuckLib.Core.View;
using Entitas;
using Gameplay.Game.Features.Position.Components;
using Gameplay.Game.View.Listeners;
using UnityEngine;
using Zenject;
using static Gameplay.Game.Services.Constants;

namespace Gameplay.Game.Services
{
    public sealed class GameViewService : IViewService<GameEntity>
    {
        private readonly DiContainer _container;

        public GameViewService(DiContainer container)
        {
            _container = container;
            CacheUiRoot();
        }

        private Transform _uiRoot;
        private readonly Transform _viewRoot = new GameObject(ViewRootName).transform;

        public void CreateView<T>(GameEntity entity, string name)
            where T : Component, IViewController<GameEntity>
        {
            CreateEmptyObject(with: name)
                .ConvertGameObjectToEntity<T>(entity);

            GameObject CreateEmptyObject(string with)
            {
                GameObject newObject = new GameObject(with);
                newObject.transform.SetParent(Root(with));

                return newObject;
            }
        }

        public void LoadView<T>(GameEntity entity, string name)
            where T : Component, IViewController<GameEntity>
        {
            LoadAndInstantiateObject(with: name)
                .ConvertGameObjectToEntity<T>(entity);

            GameObject LoadAndInstantiateObject(string with) =>
                _container.InstantiatePrefab(Resources.Load<GameObject>(with), Root(with));
        }

        public void BindExistingView<T>(GameEntity entity, string name)
            where T : Component, IViewController<GameEntity>
        {
            FindObjectInScene(with: name)
                .ConvertGameObjectToEntity<T>(entity);

            GameObject FindObjectInScene(string with) => GameObject.Find(with).gameObject;
        }

        private Transform Root(string name) => name.StartsWith("UI") ? _uiRoot : _viewRoot;

        private void CacheUiRoot()
        {
            if (_uiRoot == null)
                _uiRoot = GameObject.Find(UiRootName).transform;
        }
    }

    public static partial class CleanCodeExtensions
    {
        private static readonly Dictionary<string, Type> _map = new Dictionary<string, Type>()
        {
            {nameof(PositionComponent), typeof(PositionListener)}
        };

        public static void ConvertGameObjectToEntity<T>(this GameObject view, GameEntity entity)
            where T : Component, IViewController<GameEntity>
        {
            view
                .CreateController<T>()
                .Convert(with: entity)
                .AddListeners(@from: entity)
                .RegisterListeners(with: entity);
        }

        private static GameObject CreateController<T>(this GameObject view)
            where T : Component, IViewController<GameEntity>
        {
            if (view.GetComponent<T>() == null)
                view.AddComponent<T>();
            return view;
        }

        private static GameObject AddListeners(this GameObject view, IEntity @from)
        {
            foreach (var component in @from.GetComponents().Select(c => c.GetType().Name))
            {
                view.Do(v => v.AddComponent(_map[component]),
                    when: _map.ContainsKey(component));
            }

            return view;
        }
    }
}