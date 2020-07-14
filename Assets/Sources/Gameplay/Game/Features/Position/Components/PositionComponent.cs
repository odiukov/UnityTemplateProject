using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Gameplay.Game.Features.Position.Components
{
    [Game, Event(EventTarget.Self)]
    public class PositionComponent : IComponent
    {
        public Vector3 Value;
    }
}