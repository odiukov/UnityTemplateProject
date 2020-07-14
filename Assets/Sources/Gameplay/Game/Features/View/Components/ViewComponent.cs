using DuckLib.Core.View;
using Entitas;

namespace Gameplay.Game.Features.View.Components
{
    [Game]
    public class ViewComponent : IComponent
    {
        public IViewController<GameEntity> Controller;
    }
}