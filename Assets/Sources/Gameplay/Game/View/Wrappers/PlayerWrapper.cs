using DuckLib.Core.Extensions;
using Gameplay.Game.Blueprints;

namespace Gameplay.Game.View.Wrappers
{
    public class PlayerWrapper : UnityViewController
    {
        protected override void OnStart()
        {
            Entity.Apply(new PlayerBlueprint());
        }
    }
}