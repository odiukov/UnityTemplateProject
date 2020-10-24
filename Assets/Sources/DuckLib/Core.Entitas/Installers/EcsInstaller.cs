using System;
using System.Collections.Generic;
using Entitas;
using Entitas.VisualDebugging.Unity;
using Zenject;

namespace DuckLib.Core.Installers
{
    public class EcsInstaller : MonoInstaller
    {
        private string CommonSystemsId => $"{InstanceId}_common_systems";
        private string UpdateSystemsId => $"{InstanceId}_update_systems";
        private string FixedUpdateSystemsId => $"{InstanceId}_fixed_update_systems";
        private string InstanceId => $"{GetType().Name}_{GetInstanceID()}";

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CommonSystems>()
                .FromMethod(context =>
                    new CommonSystems(CommonSystemsId, context.Container.ResolveIdAll<ISystem>(CommonSystemsId)))
                .AsSingle();

            Container.BindInterfacesAndSelfTo<UpdateSystems>()
                .FromMethod(context =>
                    new UpdateSystems(UpdateSystemsId, context.Container.ResolveIdAll<ISystem>(UpdateSystemsId)))
                .AsSingle();

            Container.BindInterfacesAndSelfTo<FixedUpdateSystems>()
                .FromMethod(context => new FixedUpdateSystems(FixedUpdateSystemsId,
                    context.Container.ResolveIdAll<ISystem>(FixedUpdateSystemsId)))
                .AsSingle();
        }

        protected void InstallCommonSystem<T>() where T : ISystem
        {
            Container.Bind<ISystem>().WithId(CommonSystemsId).To<T>().AsSingle();
        }

        protected void InstallUpdateSystem<T>() where T : ISystem, IExecuteSystem
        {
            Container.Bind<ISystem>().WithId(UpdateSystemsId).To<T>().AsSingle();
        }

        protected void InstallFixedUpdateSystem<T>() where T : ISystem, IExecuteSystem
        {
            Container.Bind<ISystem>().WithId(FixedUpdateSystemsId).To<T>().AsSingle();
        }

        private class CommonSystems : DebugSystems, IInitializable, IDisposable
        {
            public CommonSystems(string id, IEnumerable<ISystem> systems) : base(id)
            {
                foreach (var s in systems)
                    Add(s);
            }

            public sealed override Systems Add(ISystem system)
            {
                return base.Add(system);
            }

            public void Dispose()
            {
                TearDown();
            }
        }

        private sealed class UpdateSystems : CommonSystems, ITickable
        {
            public UpdateSystems(string id, IEnumerable<ISystem> systems) : base(id, systems)
            {
            }

            public void Tick()
            {
                Execute();
                Cleanup();
            }
        }

        private sealed class FixedUpdateSystems : CommonSystems, IFixedTickable
        {
            public FixedUpdateSystems(string id, IEnumerable<ISystem> systems) : base(id, systems)
            {
            }

            public void FixedTick()
            {
                Execute();
                Cleanup();
            }
        }
    }
}