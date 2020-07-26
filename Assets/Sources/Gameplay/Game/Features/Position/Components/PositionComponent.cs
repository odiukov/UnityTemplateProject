using DuckLib.Core.Attributes;
using DuckLib.Core.View;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Gameplay.Game.View.Listeners;
using UnityEngine;

namespace Gameplay.Game.Features.Position.Components
{
    [Game, Event(EventTarget.Self), AutoListener(typeof(PositionListener))]
    public class PositionComponent : IComponent
    {
        public Vector3 Value;
    }
}