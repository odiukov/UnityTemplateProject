using Entitas;
using Gameplay.Common.Contexts;

namespace Gameplay.Common.Contexts
{
    public interface IGameContext : IContext<GameEntity>
    {
    }
}

partial class GameContext : IGameContext
{
}