using DuckLib.Core.View;
using Zenject;

namespace Gameplay.Game.Services
{
    public sealed class GameViewService : ViewService<GameEntity>
    {
        public GameViewService(DiContainer container) : base(container)
        {
        }
    }
}