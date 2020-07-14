using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Gameplay.Game.Features.Cleanup.Components
{
    [Game, Event(EventTarget.Self), Cleanup(CleanupMode.DestroyEntity)]
    public class DestructComponent : IComponent
    {

    }
}