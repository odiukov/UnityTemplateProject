using DuckLib.Core.Converters;
using UnityEngine;

namespace Gameplay.Common.Converters
{

    [RequireComponent(typeof(GameEntityListeners))]
    public sealed class AutoConvertToGameEntity : AutoConvertToEntity<GameEntity>
    {
    }
}