using DuckLib.Core.Installers;
using DuckLib.Core.View;
using Gameplay.Game.Features.Initialize.Systems;
using Gameplay.Game.Features.View.Systems;
using Gameplay.Game.Services;

namespace Gameplay.Game.Installers
{
    public class GameInstaller : EcsInstaller
    {
        public override void InstallBindings()
        {
            InstallContexts();
            InstallServices();
            InstallSystems();
            base.InstallBindings();
        }

        private void InstallContexts()
        {
            Container.BindInterfacesAndSelfTo<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.BindInterfacesAndSelfTo<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<IViewService<GameEntity>>().To<GameViewService>().AsSingle();
        }

        private void InstallSystems()
        {
            // init systems
            InstallCommonSystem<InitializeSystem>();

            // execute/reactive systems
            InstallUpdateSystem<ViewSystem>();

            // event systems
            InstallUpdateSystem<GameEventSystems>();

            // cleanup
            InstallUpdateSystem<GameCleanupSystems>();
        }
    }
}