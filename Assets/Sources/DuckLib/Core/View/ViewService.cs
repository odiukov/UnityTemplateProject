using DuckLib.Core.Extensions;
using Entitas;
using UnityEngine;
using Zenject;
using static DuckLib.Core.Extensions.Constants;

namespace DuckLib.Core.View
{
    public class ViewService<TEntity> : IViewService<TEntity> where TEntity : class, IEntity
    {
        private readonly DiContainer _container;

        protected ViewService(DiContainer container)
        {
            _container = container;
            CacheUiRoot();
        }

        private Transform _uiRoot;
        private readonly Transform _viewRoot = new GameObject(ViewRootName).transform;

        public void CreateView<T>(TEntity entity, string name)
            where T : Component, IViewController<TEntity>
        {
            CreateEmptyObject(with: name)
                .CreateController<T, TEntity>()
                .ConvertGameObjectToEntity(entity);

            GameObject CreateEmptyObject(string with)
            {
                GameObject newObject = new GameObject(with);
                newObject.transform.SetParent(Root(with));

                return newObject;
            }
        }

        public void LoadView<T>(TEntity entity, string name)
            where T : Component, IViewController<TEntity>
        {
            LoadAndInstantiateObject(with: name)
                .CreateController<T, TEntity>()
                .ConvertGameObjectToEntity(entity);

            GameObject LoadAndInstantiateObject(string with) =>
                _container.InstantiatePrefab(Resources.Load<GameObject>(with), Root(with));
        }

        public void BindExistingView<T>(TEntity entity, string name)
            where T : Component, IViewController<TEntity>
        {
            FindObjectInScene(with: name)
                .CreateController<T, TEntity>()
                .ConvertGameObjectToEntity(entity);

            GameObject FindObjectInScene(string with) => GameObject.Find(with).gameObject;
        }

        private Transform Root(string name) => name.StartsWith("UI") ? _uiRoot : _viewRoot;

        private void CacheUiRoot()
        {
            if (_uiRoot == null)
                _uiRoot = GameObject.Find(UiRootName).transform;
        }
    }
}