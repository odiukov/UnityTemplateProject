using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Gameplay.Game.Features.Collision.Components
{
    [Game]
    public class CollisionIdComponent : IComponent
    {
        [EntityIndex] public int Value;
    }
}