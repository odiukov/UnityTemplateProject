using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Gameplay.Game.Features.Collision.Components
{
    [Game]
    public class IdComponent : IComponent
    {
        [PrimaryEntityIndex] public int Value;
    }
}