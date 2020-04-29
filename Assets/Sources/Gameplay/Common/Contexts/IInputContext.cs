using Entitas;
using Gameplay.Common.Contexts;

namespace Gameplay.Common.Contexts
{
    public interface IInputContext : IContext<InputEntity>
    {
    }
}

partial class InputContext : IInputContext
{
}