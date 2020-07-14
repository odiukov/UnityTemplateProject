using DuckLib.Core.Converters;
using UnityEngine;

namespace Gameplay.Common.Converters
{
    [RequireComponent(typeof(InputEntityListeners))]
    public sealed class AutoConvertToInputEntity : AutoConvertToEntity<InputEntity>
    {
    }
}