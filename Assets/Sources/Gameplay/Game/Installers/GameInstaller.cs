using DuckLib.Core.Installers;

namespace Gameplay.Game.Installers
{
    public class GameInstaller : EcsInstaller
    {
        public override void InstallBindings()
        {
            InstallContexts();
            InstallSystems();
            base.InstallBindings();
        }

        private void InstallContexts()
        {
            Container.BindInterfacesAndSelfTo(typeof(GameContext)).FromInstance(Contexts.sharedInstance.game);
            Container.BindInterfacesAndSelfTo(typeof(InputContext)).FromInstance(Contexts.sharedInstance.input);
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
        }

        private void InstallSystems()
        {
        }
    }
}