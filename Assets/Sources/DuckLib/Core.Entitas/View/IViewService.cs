using Entitas;
using UnityEngine;

namespace DuckLib.Core.View
{
    public interface IViewService<TEntity> where TEntity : class, IEntity
    {
        void CreateView<T>(TEntity entity, string name) where T : Component, IViewController<TEntity>;
        void LoadView<T>(TEntity entity, string name) where T : Component, IViewController<TEntity>;

        void BindExistingView<T>(TEntity entity, string name)
            where T : Component, IViewController<TEntity>;
    }
}