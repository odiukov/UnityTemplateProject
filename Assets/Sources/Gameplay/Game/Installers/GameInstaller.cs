using DuckLib.Core.Installers;

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
            Container.BindInterfacesAndSelfTo<GameContext>().FromInstance(Contexts.sharedInstance.game);
            Container.BindInterfacesAndSelfTo<InputContext>().FromInstance(Contexts.sharedInstance.input);
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
        }

        private void InstallServices()
        {
        }

        private void InstallSystems()
        {
        }
    }
}