using System.Collections.Generic;
using DuckLib.Core.View;
using Entitas;

namespace DuckLib.Core.Services
{
    public abstract class CollidingViewRegister<TEntity> : IRegisterService<IViewController<TEntity>>
        where TEntity : class, IEntity
    {
        private readonly Dictionary<int, IViewController<TEntity>> _controllerByInstanceId =
            new Dictionary<int, IViewController<TEntity>>();

        public IViewController<TEntity> Register(int instanceId, IViewController<TEntity> @object)
        {
            _controllerByInstanceId[instanceId] = @object;
            return @object;
        }

        public void Unregister(int instanceId, IViewController<TEntity> @object)
        {
            if (_controllerByInstanceId.ContainsKey(instanceId))
                _controllerByInstanceId.Remove(instanceId);
        }

        public IViewController<TEntity> Take(int key) =>
            _controllerByInstanceId.TryGetValue(key, out var behaviour)
                ? behaviour
                : null;
    }
}