using DuckLib.Core.Installers;
using Gameplay.Common.Contexts;

namespace Gameplay.Game.Installers
{
    public class GameInstaller : EcsInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
            Container.Bind<IGameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<IInputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
        }
    }
}